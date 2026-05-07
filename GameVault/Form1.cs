using System;
using System.Drawing;
using System.Windows.Forms;
using GameVault.Models;
using GameVault.BLL; // Import our BLL

namespace GameVault
{
    public partial class Form1 : Form
    {
        // 1. Instantiate the Logic Layer and create the control variable
        private GameBLL businessLogic = new GameBLL();
        private int selectedId = 0; // Starts at 0 (Create Mode)

        public Form1()
        {
            InitializeComponent();
            SubscribeEvents();
            ApplyStyles();
        }

        // Helper to wire up events that are missing from the designer
        private void SubscribeEvents()
        {
            // Only hook up events that are truly missing from Form1.Designer.cs
            this.Load += Form1_Load;
            dgvGames.CellClick += dgvGames_CellClick;
        }

        // Apply modern styles so it doesn't look too simple
        private void ApplyStyles()
        {
            this.BackColor = Color.FromArgb(240, 244, 248); // Clean light background
            this.Font = new Font("Segoe UI", 10);
            this.Text = "GameVault Manager";
            this.StartPosition = FormStartPosition.CenterScreen;

            // DataGridView Modern Styles
            dgvGames.BackgroundColor = Color.White;
            dgvGames.BorderStyle = BorderStyle.None;
            dgvGames.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvGames.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219); // Blue selection
            dgvGames.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvGames.DefaultCellStyle.BackColor = Color.White;
            dgvGames.DefaultCellStyle.ForeColor = Color.FromArgb(50, 50, 50);
            dgvGames.EnableHeadersVisualStyles = false;
            dgvGames.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvGames.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185); // Header blue
            dgvGames.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvGames.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvGames.ColumnHeadersHeight = 35;
            dgvGames.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Select the entire row clicking anywhere
            dgvGames.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGames.ReadOnly = true;
            dgvGames.RowHeadersVisible = false;

            // Buttons Styling
            StyleButton(btnSave, Color.FromArgb(46, 204, 113)); // Emerald Green
            StyleButton(btnDelete, Color.FromArgb(231, 76, 60)); // Alizarin Red
        }

        private void StyleButton(Button btn, Color bgColor)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = bgColor;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        // This event occurs as soon as the application opens
        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshTable();
        }

        // Cleanup method used frequently
        private void RefreshTable()
        {
            // Request the list from BLL and bind it to the table
            dgvGames.DataSource = businessLogic.GetAllGames();

            // Clear text boxes
            txtTitle.Clear();
            txtGenre.Clear();
            txtPrice.Clear();

            // Reset the ID to 0 to ensure "Create" mode is active
            selectedId = 0;
        }

        // THE SAVE BUTTON (Dual Logic)
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate Price input safely before assigning. 
                // We use InvariantCulture and replace commas with dots to avoid regional format crashes (like "400.00" vs "400,00").
                string priceInput = txtPrice.Text.Replace(',', '.');
                if (!decimal.TryParse(priceInput, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal priceValue))
                {
                    MessageBox.Show("Please enter a valid number for the price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Stop the save process
                }

                // Build the object with what the user wrote
                Game game = new Game();
                game.Id = selectedId; // If 0 it inserts, if >0 it updates
                game.Title = txtTitle.Text;
                game.Genre = txtGenre.Text;
                game.Price = priceValue;

                // Send it to the BLL to validate and save
                businessLogic.SaveGame(game);

                MessageBox.Show("Game saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshTable(); // Refresh to see the changes
            }
            catch (Exception ex)
            {
                // If the BLL throws an error (e.g. negative price), catch it and show it here
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // THE TABLE EVENT (To select a game)
        private void dgvGames_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure they didn't click on the header
            if (e.RowIndex >= 0)
            {
                // Capture the ID of the selected row and switch to "Update" mode
                selectedId = Convert.ToInt32(dgvGames.Rows[e.RowIndex].Cells["Id"].Value);

                // Pass the data to the TextBoxes
                txtTitle.Text = dgvGames.Rows[e.RowIndex].Cells["Title"].Value.ToString();
                txtGenre.Text = dgvGames.Rows[e.RowIndex].Cells["Genre"].Value.ToString();
                txtPrice.Text = dgvGames.Rows[e.RowIndex].Cells["Price"].Value.ToString();
            }
        }

        // THE DELETE BUTTON (Safe flow)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Check if something is selected
                if (selectedId == 0)
                {
                    MessageBox.Show("Please select a game from the table first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Ask user for confirmation
                DialogResult answer = MessageBox.Show("Are you sure you want to delete this game?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (answer == DialogResult.Yes)
                {
                    // 3. Delete, notify, and refresh
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