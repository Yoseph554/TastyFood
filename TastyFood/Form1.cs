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
            // Opcional: Puedes inicializar el textBox1 para que muestre "Lps 0.00" al inicio.
            // Asegúrate de que textBox1 sea de solo lectura si no quieres que el usuario lo edite.
            textBox1.Text = "Lps 0.00";
        }

        // --- Ejemplo de cómo tus botones de productos (como button1) añadirían ítems ---
        private void button1_Click(object sender, EventArgs e)
        {
            // Este es un EJEMPLO. Cada botón de producto debería tener su propio nombre de producto.
            string productoSeleccionado = "Combo 1"; // CAMBIA ESTO para cada botón de producto

            MenuItem itemEncontrado = menuDelRestaurante.Menu.FirstOrDefault(item => item.Name == productoSeleccionado);

            if (itemEncontrado != null)
            {
                if (ventaEnMemoria.ContainsKey(productoSeleccionado))
                {
                    ventaEnMemoria[productoSeleccionado]++;
                }
                else
                {
                    ventaEnMemoria.Add(productoSeleccionado, 1);
                }

                // Opcional: Actualizar un display para ver los ítems agregados (ej. un ListBox)
                ActualizarDisplayVenta();
            }
            else
            {
                MessageBox.Show("El producto seleccionado no se encontró en el menú.", "Error de Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // --- Método auxiliar para actualizar la visualización de la venta (ej. un ListBox) ---
        private void ActualizarDisplayVenta()
        {
            // Asume que tienes un ListBox llamado 'lstVentaActual' en tu formulario.
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
                // Si tienes un Label para el total, también podrías actualizarlo aquí
                // si no quieres que textBox1 sea el único lugar para el total.
                // if (this.Controls.Find("lblTotalVenta", true).FirstOrDefault() is Label lblTotalVenta)
                // {
                //     lblTotalVenta.Text = $"Total: Lps {subtotal:F2}";
                // }
            }
            else
            {
                // Si no tienes un ListBox, puedes ver el estado en la ventana de Salida de Visual Studio.
                System.Diagnostics.Debug.WriteLine("Venta Actual:");
                foreach (var entry in ventaEnMemoria)
                {
                    System.Diagnostics.Debug.WriteLine($"- {entry.Key}: {entry.Value} unidades");
                }
            }
        }

        // --- Lógica para el botón "Pagar" (button14) ---
        private void button14_Click(object sender, EventArgs e)
        {
            if (ventaEnMemoria.Count == 0)
            {
                MessageBox.Show("No hay productos en la venta actual para pagar.", "Venta Vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "Lps 0.00"; // Asegura que el textbox se resetee si no hay venta
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

            // Muestra el total en el textBox1
            // Usamos ":F2" para formatear el número con dos decimales.
            textBox1.Text = $"Lps {totalPagar:F2}";

            // Opcional: Mostrar un mensaje con el detalle de la venta antes de limpiar
            MessageBox.Show(detalleVenta + $"\nTotal a Pagar: Lps {totalPagar:F2}", "Venta Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Una vez que la venta se procesa, limpiamos la venta en memoria para la siguiente.
            ventaEnMemoria.Clear();
            ActualizarDisplayVenta(); // Esto también debería limpiar el ListBox si lo estás usando.
        }

        // --- El método textBox1_TextChanged ---
        // Este método se activa cada vez que el texto en textBox1 cambia.
        // Generalmente, no pondrías lógica de "pago" aquí, sino que la usarías para
        // validación de entrada o formato si el usuario pudiera escribir en él.
        // En este caso, como lo actualizaremos programáticamente, lo dejamos vacío
        // a menos que tengas otra función para él.
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Por ahora, este método puede quedarse vacío.
            // Si en el futuro necesitas que algo suceda cuando el total cambie visualmente
            // (por ejemplo, cambiar el color del texto si el total es muy alto),
            // lo pondrías aquí.
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        // --- Asegúrate de que Class1 y MenuItem estén definidos ---
        /*
        public class Class1
        {
            public List<MenuItem> Menu { get; set; }

            public Class1()
            {
                Menu = new List<MenuItem>
                {
                    new MenuItem { Name = "Combo 1", Price = 125 },
                    new MenuItem { Name = "Combo 2", Price = 130 },
                    new MenuItem { Name = "Pechuga", Price = 45 },
                    new MenuItem { Name = "Cadera", Price = 35 },
                    new MenuItem { Name = "Ala", Price = 24 },
                    new MenuItem { Name = "Pierna", Price = 24 },
                    new MenuItem { Name = "Salsa", Price = 20 },
                    new MenuItem { Name = "Frescos", Price = 25 },
                    new MenuItem { Name = "Tajada con pollo", Price = 65 },
                    new MenuItem { Name = "Poquete Tortilla", Price = 15 },
                    new MenuItem { Name = "Granita", Price = 40 },
                    new MenuItem { Name = "Jugos", Price = 25 }
                };
            }
        }

        public class MenuItem
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
        }
        */
    }
}