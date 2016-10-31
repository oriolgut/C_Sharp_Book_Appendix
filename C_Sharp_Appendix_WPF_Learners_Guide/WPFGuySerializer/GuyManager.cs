using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;

namespace WPFGuySerializer
{
    class GuyManager : INotifyPropertyChanged
    {
        private Guy _joe = new Guy("Joe", 37, 176.22M);
        private Guy _bob = new Guy("Bob", 45, 4.68M);
        private Guy _ed = new Guy("Ed", 43, 37.51M);

        public Guy Joe
        {
            get
            {
                return _joe;
            }
        }

        public Guy Bob
        {
            get
            {
                return _bob;
            }
        }

        public Guy Ed
        {
            get
            {
                return _ed;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public Guy NewGuy { get; set; }
        public string GuyFile { get; set; }

        public void ReadGuy()
        {
            if (String.IsNullOrEmpty(GuyFile))
            {
                return;
            }
            using(Stream inputStream = File.OpenRead(GuyFile))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(Guy));
                NewGuy = serializer.ReadObject(inputStream) as Guy;
            }
            OnPropertyChanged(nameof(NewGuy));
        }

        public void WriteGuy(Guy guyToWrite)
        {
            GuyFile = Path.GetFullPath($"{guyToWrite.Name}.xml");

            if (File.Exists(GuyFile))
            {
                File.Delete(GuyFile);
            }

            using(Stream outputStream = File.OpenWrite(GuyFile))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(Guy));
                serializer.WriteObject(outputStream, guyToWrite);
            }

            OnPropertyChanged(nameof(GuyFile));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
