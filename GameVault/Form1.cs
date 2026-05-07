using System;
using System.Windows.Forms;
using GameVault.Models;
using GameVault.BLL; // Importamos nuestro "Chef"

namespace GameVault
{
    public partial class Form1 : Form
    {
        // 1. Instanciamos nuestra Capa Lógica y creamos la variable centinela
        private GameBLL businessLogic = new GameBLL();
        private int selectedId = 0; // Empieza en 0 (Modo: Crear)

        public Form1()
        {
            InitializeComponent();
        }

        // Este evento ocurre apenas se abre el programa
        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshTable();
        }

        // Método de limpieza que usaremos a cada rato
        private void RefreshTable()
        {
            // Le pedimos la lista al BLL y la metemos a la tabla
            dgvGames.DataSource = businessLogic.GetAllGames();

            // Limpiamos las cajas de texto
            txtTitle.Clear();
            txtGenre.Clear();
            txtPrice.Clear();

            // Reseteamos el ID a 0 para asegurar que el modo "Crear" esté activo
            selectedId = 0;
        }

        // EL BOTÓN SAVE (La Lógica Dual)
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Armamos el "molde" con lo que el usuario escribió
                Game game = new Game();
                game.Id = selectedId; // Si es 0 insertará, si es >0 actualizará
                game.Title = txtTitle.Text;
                game.Genre = txtGenre.Text;
                game.Price = Convert.ToDecimal(txtPrice.Text);

                // Se lo mandamos al Chef (BLL) para que valide y guarde
                businessLogic.SaveGame(game);

                MessageBox.Show("Game saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshTable(); // Refrescamos para ver los cambios
            }
            catch (Exception ex)
            {
                // Si el BLL lanza un error (ej. precio negativo), lo atrapamos y mostramos aquí
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // EL EVENTO DE LA TABLA (Para seleccionar un juego)
        private void dgvGames_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificamos que no hayan hecho clic en el encabezado
            if (e.RowIndex >= 0)
            {
                // Atrapamos el ID de la fila seleccionada y cambiamos al modo "Actualizar"
                selectedId = Convert.ToInt32(dgvGames.Rows[e.RowIndex].Cells["Id"].Value);

                // Pasamos los datos a los TextBox
                txtTitle.Text = dgvGames.Rows[e.RowIndex].Cells["Title"].Value.ToString();
                txtGenre.Text = dgvGames.Rows[e.RowIndex].Cells["Genre"].Value.ToString();
                txtPrice.Text = dgvGames.Rows[e.RowIndex].Cells["Price"].Value.ToString();
            }
        }

        // EL BOTÓN DELETE (El flujo seguro)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Verificamos si hay algo seleccionado
                if (selectedId == 0)
                {
                    MessageBox.Show("Please select a game from the table first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Pedimos confirmación al usuario
                DialogResult answer = MessageBox.Show("Are you sure you want to delete this game?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (answer == DialogResult.Yes)
                {
                    // 3. Mandamos borrar, avisamos y limpiamos
                    businessLogic.DeleteGame(selectedId);
                    MessageBox.Show("Game deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}