using DPFP;
using DPFP.Capture;
using DPFP.Processing;
using DPFP.Verification;
using DPFP.Error;
using MetroFramework.Forms;
using System;
using System.IO;
using System.Windows.Forms;
using Serilog;

namespace WinForms.LectorHuella
{
    public partial class Buscar : MetroForm, DPFP.Capture.EventHandler
    {      
        private Capture captura;
        private Template plantillaHuella;
        private Verification verificar;

        #region Event Form        
        public Buscar()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Buscar_Load(object sender, EventArgs e)
        {
            textBoxId.Enabled = false;
            textBoxName.Enabled = false;
            textBoxAlta.Enabled = false;
            Log.Information("BUSCAR LOAD");
            Connect();
        }

        private void Buscar_FormClosed(object sender, FormClosedEventArgs e)
        {
            Log.Information("BUSCAR CLOSED");
            Stop();
        }
        #endregion

        #region Button's    
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
      
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
                    plantillaHuella = new Template();
                    verificar = new Verification();
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
                }
                catch (Exception ex)
                {
                    Log.Information($"Ha ocurrido una Exception: {ex.ToString()}");
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
                }
                catch (Exception ex)
                {
                    Log.Information($"Ha ocurrido una Exception: {ex.ToString()}");
                }
            }
        }

        void ResetControl()
        {
            textBoxId.Text = string.Empty;
            textBoxName.Text = string.Empty;
            textBoxAlta.Text = string.Empty;
        }

        #region EventHandler Members        
        public void OnComplete(object Capture, string ReaderSerialNumber, Sample Sample)
        {
            Log.Information("=== BUSCAR OnComplete ===");
            var result = new Verification.Result();
            var resultadoSP = string.Empty;

            pictureBoxBuscar.Image = Helper.ConvertSampleToBitmap(Sample);
            var caracteristicas = Helper.ObtenerCaracteristicasHuella(Sample, DataPurpose.Verification);

            if (caracteristicas != null)
            {
                try
                {
                    Log.Information("AccessData.VerificarHuella");
                    var usuarios = AccessData.VerificarHuella(out resultadoSP);
                    Log.Information(resultadoSP);

                    foreach (var user in usuarios)
                    {
                        Log.Information(user.nombre);

                        var ms = new MemoryStream(user.huella);
                        plantillaHuella.DeSerialize(ms.ToArray());
                        verificar.Verify(caracteristicas, plantillaHuella, ref result);

                        if (result.Verified)
                        {
                            Log.Information("MATCH!!!!");
                            Log.Information($"ID: {user.id} ==> USUARIO: {user.nombre}");

                            textBoxId.Text = user.id.ToString();
                            textBoxName.Text = user.nombre;
                            textBoxAlta.Text = user.fechaAlta.ToString();
                            break;
                        }
                        else
                            ResetControl();
                    }
                }
                catch (Exception ex)
                {
                    Log.Information($"Ha ocurrido una Exception: {ex.ToString()}");
                    var error = new SDKException(ex, 0, "");
                }
            }
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            Log.Information("=== BUSCAR OnFingerGone ===");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            var des = new ReaderDescription(ReaderSerialNumber);
            Log.Information("=== BUSCAR OnFingerTouch ===");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            Log.Information("=== BUSCAR OnReaderConnect ===");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            Log.Information("=== BUSCAR OnReaderDisconnect ===");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, CaptureFeedback CaptureFeedback)
        {
            Log.Information("=== BUSCAR OnSampleQuality ===");
        }
        #endregion
    }
}