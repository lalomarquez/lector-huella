using DPFP;
using DPFP.Capture;
using DPFP.Error;
using DPFP.Processing;
using MetroFramework.Forms;
using Serilog;
using System;
using System.Configuration;
//using System.Configuration;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WinForms.LectorHuella
{
    public partial class Pantalla : MetroForm, DPFP.Capture.EventHandler
    {
        private Capture captura;
        private Enrollment inscripcion;
        private Template plantilla;
        private Model model;
        private delegate void Intentos(string mensaje);
        private delegate void Controles();
        //private LogManager bitacora;

        #region Event Form
        public Pantalla()
        {
            InitializeComponent();
            //bitacora = new LogManager();     
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(ConfigurationManager.AppSettings["fileLog"], rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("public partial class Pantalla");
        }
        private void Form1_Load(object sender, EventArgs e)
        {            
            Log.Information("PANTALLA LOAD");
            Connect();
        }

        private void Pantalla_Activated(object sender, EventArgs e)
        {
            Log.Information("PANTALLA ACTIVATED");           
            //Connect();
        }  

        private void Pantalla_Leave(object sender, EventArgs e)
        {
            Log.Information("PANTALLA LEAVE");
            Stop();
        }

        private void Pantalla_FormClosed(object sender, FormClosedEventArgs e)
        {
            Log.Information("PANTALLA CLOSED");
            Log.Information("PANTALLA CLOSED");
            Stop();
            Log.CloseAndFlush();
        }
        #endregion

        #region Delegados        
        public void ContadorIntentos(string mensaje)
        {
            if (lblIntentos.InvokeRequired)
            {
                var delegado = new Intentos(ContadorIntentos);
                this.Invoke(delegado, new Object[] { mensaje });
            }
            else
                lblIntentos.Text = mensaje;
        }

        public void HabilitarControles()
        {
            if (btnGuardar.InvokeRequired)
            {
                var delegado = new Controles(HabilitarControles);
                this.Invoke(delegado, new Object[] { });
            }
            else
            {
                textBoxName.Enabled = true;
                btnGuardar.Enabled = true;
            }
        }
        #endregion

        #region Button's        
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBoxName.Text) && plantilla.Bytes != null)
                {
                    model = new Model();
                    model.nombre = textBoxName.Text;
                    using (var ms = new MemoryStream(plantilla.Bytes))
                        model.huella = ms.ToArray();

                    Log.Information("AccessData.InsertUser");
                    var resultado = AccessData.InsertUser(model);

                    MessageBox.Show(resultado, "Resultado");
                    DialogResult accionBoton = MessageBox.Show("Quieres realizar otra captura de huella?", "Captura", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (accionBoton == DialogResult.OK)
                    {
                        textBoxName.Text = string.Empty;
                        pictureBoxHuella.Image = null;
                        Connect();
                    }
                    else
                    {
                        textBoxName.Text = string.Empty;
                        textBoxName.Enabled = false;
                        btnGuardar.Enabled = false;
                    }
                }
                else
                    MessageBox.Show("Debes capturar un [Nombre]");
            }
            catch (Exception ex)
            {
                Log.Information($"Ha ocurrido una Exception: {ex.ToString()}");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Stop();
            var busqueda = new Buscar();
            busqueda.ShowDialog();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Stop();
            textBoxName.Text = string.Empty;
            pictureBoxHuella.Image = null;
            plantilla = null;
            Connect();
        }
        #endregion

        #region Metodos Generales
        void Connect()
        {
            Init();
            Start();
        }

        void Init()
        {
            try
            {
                captura = new Capture();
                if (captura != null)
                {
                    captura.EventHandler = this;
                    Log.Information($"Se ha instanciado el objeto [Capture]");

                    inscripcion = new Enrollment();
                    var sb = new StringBuilder();
                    sb.AppendFormat("Necesitas pasar el dedo {0} veces.", inscripcion.FeaturesNeeded);
                    lblIntentos.Text = sb.ToString();
                }
                else
                    Log.Information("No se ha podido inicializar una instancia de [Capture]");
            }
            catch (Exception ex)
            {
                Log.Information($"Ha ocurrido una Exception: {ex.ToString()}");
            }
        }

        void Start()
        {
            if (captura != null)
            {
                try
                {
                    captura.StartCapture();
                    Log.Information("Se ha inicializado el lector de huella, escanea tu dedo!!!");
                }
                catch (Exception ex)
                {
                    Log.Information($"Ha ocurrido una Exception: {ex.ToString()}");
                    Log.Error($"Ha ocurrido una Exception: {ex.ToString()}");
                }
            }
        }

        void Stop()
        {
            if (captura != null)
            {
                try
                {
                    captura.StopCapture();
                    Log.Information("Ha finalizado el proceso.");
                }
                catch (Exception ex)
                {
                    Log.Information($"Ha ocurrido una Exception: {ex.ToString()}");
                }
            }
        }     

        public void ProcesarHuella(Sample Sample)
        {
            var caracteristicas = Helper.ObtenerCaracteristicasHuella(Sample, DataPurpose.Enrollment);
            if (caracteristicas != null)
            {
                try
                {
                    inscripcion.AddFeatures(caracteristicas);
                }
                catch (Exception ex)
                {
                    Log.Information("Ha ocurrido algo inesperado ;( " + ex.ToString());
                    Stop();
                }
                finally
                {
                    var sb = new StringBuilder();
                    sb.AppendFormat("Necesitas pasar el dedo {0} veces.", inscripcion.FeaturesNeeded);
                    ContadorIntentos(sb.ToString());

                    switch (inscripcion.TemplateStatus)
                    {
                        case Enrollment.Status.Ready:   // report success and stop capturing
                            plantilla = inscripcion.Template;
                            Stop();
                            HabilitarControles();
                            break;
                        case Enrollment.Status.Failed:  // report failure and restart capturing
                            inscripcion.Clear();
                            Stop();
                            Start();
                            break;
                    }
                }
            }
        }
        #endregion

        #region EventHandler Members
        public void OnComplete(object Capture, string ReaderSerialNumber, Sample Sample)
        {
            Log.Information($"=== Pantalla Main OnComplete ===");
            pictureBoxHuella.Image = Helper.ConvertSampleToBitmap(Sample);
            ProcesarHuella(Sample);
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            Log.Information($"=== Pantalla Main OnFingerGone ===");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            Log.Information($"=== Pantalla Main OnFingerTouch ===");
            var des = new ReaderDescription(ReaderSerialNumber);
            Log.Information($"[{des.ProductName}]");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            Log.Information($"=== Pantalla Main OnReaderConnect ===");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            Log.Information($"=== Pantalla Main OnReaderDisconnect ===");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, CaptureFeedback CaptureFeedback)
        {
            Log.Information($"=== Pantalla Main OnSampleQuality ===");
        }
        #endregion
    }
}