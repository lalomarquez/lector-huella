using DPFP;
using DPFP.Capture;
using DPFP.Processing;
using Serilog;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;

namespace WinForms.LectorHuella
{
    public static class Helper
    {        
        public static Bitmap ConvertSampleToBitmap(Sample Sample)
        {
            var Convertor = new SampleConversion();
            Bitmap bitmap = null;
            Convertor.ConvertToPicture(Sample, ref bitmap);
            return bitmap;
        }

        public static FeatureSet ObtenerCaracteristicasHuella(Sample Sample, DataPurpose Purpose)
        {
            var Extractor = new FeatureExtraction();
            var Feedback = CaptureFeedback.None;
            var caracteristicas = new FeatureSet();

            Extractor.CreateFeatureSet(Sample, Purpose, ref Feedback, ref caracteristicas);
            if (Feedback == CaptureFeedback.Good)
                return caracteristicas;
            else
                return null;
        }

        public static string GetConn()
        {            
            string conn = ConfigurationManager.ConnectionStrings["conn"].ToString();
            Log.Information($"GetConn>> {conn}");

            return (conn != null) ? conn : string.Empty;
        }

        public static string GetEnumDescription(this Enum value)
        {
            var enumType = value.GetType();
            var field = enumType.GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length == 0 ? value.ToString() : ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}