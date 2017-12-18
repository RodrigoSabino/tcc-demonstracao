using System.Collections.Generic;

namespace TccWebApp.Models
{
    public class Algorithm
    {
        public string id { get; set; }
        public string name { get; set; }

        public List<Algorithm> AlgorithmList()
        {
            return new List<Algorithm>
            {
                new Algorithm { id = "knn", name = "KNN" },
                new Algorithm { id = "nn", name = "MLP" },
                new Algorithm { id = "svm", name = "SVM" },
                new Algorithm { id = "voter", name = "Votador" },
                new Algorithm { id = "knne", name = "KNN-energy" },
                new Algorithm { id = "nne", name = "MLP-energy" },
                new Algorithm { id = "svme", name = "SVM-energy" },
                new Algorithm { id = "votere", name = "Votador-energy" }
            };
        }
    }
}