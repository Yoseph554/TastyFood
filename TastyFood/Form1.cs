namespace TastyFood
{
    public partial class Form1 : Form
    {
        // Esto mantendrá un registro de la venta actual en memoria.
        private Dictionary<string, int> ventaEnMemoria = new Dictionary<string, int>();

        // Necesitamos una instancia de nuestra clase Class1 para acceder al menú.
        private Class1 menuDelRestaurante = new Class1();

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "Lps 0.00";
        }

        // --- Botones de Productos (Sin cambios, solo por contexto) ---
        private void button1_Click(object sender, EventArgs e)
        {
            string productoSeleccionado = "Combo 1";
            AgregarProductoAVenta(productoSeleccionado);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string productoSeleccionado = "Combo 2";
            AgregarProductoAVenta(productoSeleccionado);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string productoSeleccionado = "Pechuga";
            AgregarProductoAVenta(productoSeleccionado);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string productoSeleccionado = "Cadera";
            AgregarProductoAVenta(productoSeleccionado);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string productoSeleccionado = "Ala";
            AgregarProductoAVenta(productoSeleccionado);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string productoSeleccionado = "Pierna";
            AgregarProductoAVenta(productoSeleccionado);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string productoSeleccionado = "Salsa";
            AgregarProductoAVenta(productoSeleccionado);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string productoSeleccionado = "Frescos";
            AgregarProductoAVenta(productoSeleccionado);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string productoSeleccionado = "Tajada con pollo";
            AgregarProductoAVenta(productoSeleccionado);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string productoSeleccionado = "Paquete Tortilla";
            AgregarProductoAVenta(productoSeleccionado);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string productoSeleccionado = "Granita";
            AgregarProductoAVenta(productoSeleccionado);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string productoSeleccionado = "Jugos";
            AgregarProductoAVenta(productoSeleccionado);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string productoSeleccionado = "Papas";
            AgregarProductoAVenta(productoSeleccionado);
        }

        // --- Método auxiliar unificado para agregar productos a la venta (sin cambios) ---
        private void AgregarProductoAVenta(string nombreProducto)
        {
            MenuItem itemEncontrado = menuDelRestaurante.Menu.FirstOrDefault(item => item.Name == nombreProducto);

            if (itemEncontrado != null)
            {
                if (ventaEnMemoria.ContainsKey(nombreProducto))
                {
                    ventaEnMemoria[nombreProducto]++;
                }
                else
                {
                    ventaEnMemoria.Add(nombreProducto, 1);
                }

                ActualizarDisplayVenta();
            }
            else
            {
                MessageBox.Show($"El producto '{nombreProducto}' no se encontró en el menú.", "Error de Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // --- Método auxiliar para actualizar la visualización de la venta (sin cambios) ---
        private void ActualizarDisplayVenta()
        {
            if (this.Controls.Find("lstVentaActual", true).FirstOrDefault() is ListBox lstVentaActual)
            {
                lstVentaActual.Items.Clear();
                decimal subtotal = 0;

                foreach (var entry in ventaEnMemoria)
                {
                    string nombreProducto = entry.Key;
                    int cantidad = entry.Value;

                    MenuItem itemEncontrado = menuDelRestaurante.Menu.FirstOrDefault(item => item.Name == nombreProducto);

                    if (itemEncontrado != null)
                    {
                        decimal precioUnitario = itemEncontrado.Price;
                        decimal precioTotalItem = cantidad * precioUnitario;
                        lstVentaActual.Items.Add($"{nombreProducto} x {cantidad} = Lps {precioTotalItem:F2}");
                        subtotal += precioTotalItem;
                    }
                }
                textBox1.Text = $"Lps {subtotal:F2}";
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Venta Actual:");
                foreach (var entry in ventaEnMemoria)
                {
                    System.Diagnostics.Debug.WriteLine($"- {entry.Key}: {entry.Value} unidades");
                }
            }
        }

        // --- Lógica para el botón "Factura" (button13) ---
        private void button13_Click(object sender, EventArgs e)
        {
            if (ventaEnMemoria.Count == 0)
            {
                MessageBox.Show("No hay productos en la venta actual para generar una factura.", "Venta Vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal totalActual = 0;
            System.Text.StringBuilder facturaBuilder = new System.Text.StringBuilder();

            facturaBuilder.AppendLine("--- Detalle de Venta ---");
            facturaBuilder.AppendLine($"Fecha: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            facturaBuilder.AppendLine("------------------------");

            foreach (var entry in ventaEnMemoria)
            {
                string nombreProducto = entry.Key;
                int cantidad = entry.Value;

                MenuItem itemEncontrado = menuDelRestaurante.Menu.FirstOrDefault(item => item.Name == nombreProducto);

                if (itemEncontrado != null)
                {
                    decimal precioUnitario = itemEncontrado.Price;
                    decimal precioTotalItem = cantidad * precioUnitario;
                    totalActual += precioTotalItem;
                    facturaBuilder.AppendLine($"{nombreProducto,-20} x {cantidad,-3} = Lps {precioTotalItem,8:F2}"); // Formato para alinear
                }
            }

            facturaBuilder.AppendLine("------------------------");
            facturaBuilder.AppendLine($"Total: Lps {totalActual:F2}");
            facturaBuilder.AppendLine("------------------------");
            facturaBuilder.AppendLine("¡Gracias por su compra!");

            // Copiar la factura al portapapeles
            try
            {
                Clipboard.SetText(facturaBuilder.ToString());
                MessageBox.Show("¡Factura copiada al portapapeles!\nAhora puedes pegarla en el Bloc de Notas o cualquier otro lugar.", "Factura Lista", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al copiar la factura: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Lógica para el botón "Pagar" (button14) (sin cambios) ---
        private void button14_Click(object sender, EventArgs e)
        {
            if (ventaEnMemoria.Count == 0)
            {
                MessageBox.Show("No hay productos en la venta actual para pagar.", "Venta Vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "Lps 0.00";
                return;
            }

            decimal totalPagar = 0;
            string detalleVenta = "Detalle de la Venta:\n";

            foreach (var entry in ventaEnMemoria)
            {
                string nombreProducto = entry.Key;
                int cantidad = entry.Value;

                MenuItem itemEncontrado = menuDelRestaurante.Menu.FirstOrDefault(item => item.Name == nombreProducto);

                if (itemEncontrado != null)
                {
                    decimal precioUnitario = itemEncontrado.Price;
                    decimal precioTotalItem = cantidad * precioUnitario;
                    totalPagar += precioTotalItem;
                    detalleVenta += $"{nombreProducto} (x{cantidad}) - Lps {precioTotalItem:F2}\n";
                }
            }

            textBox1.Text = $"Lps {totalPagar:F2}";
            MessageBox.Show(detalleVenta + $"\nTotal a Pagar: Lps {totalPagar:F2}", "Venta Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ventaEnMemoria.Clear();
            ActualizarDisplayVenta();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Este método se deja vacío a menos que necesites lógica específica al cambiar el texto.
        }
    }
}