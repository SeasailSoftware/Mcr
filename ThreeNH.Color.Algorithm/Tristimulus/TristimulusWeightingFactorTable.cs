using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using ThreeNH.Color.Enums;

namespace ThreeNH.Color.Algorithm.Tristimulus
{
    public class TristimulusWeightingFactorTable
    {
        private static readonly string TRISTIMULUS_WEIGHTING_WEIGHTING_FACTORS_5NM_TABLE_File = "TristimulusWeightingFactorTable_5nm.json";
        private static readonly string TRISTIMULUS_WEIGHTING_WEIGHTING_FACTORS_10NM_TABLE_File = "TristimulusWeightingFactorTable_10nm.json";

        static TristimulusWeightingFactorTable()
        {
            TRISTIMULUS_WEIGHTING_WEIGHTING_FACTORS_5NM_TABLE = new TristimulusWeightingFactorTable()
            {
                _factors = JsonConvert.DeserializeObject<List<TristimulusWeightingFactor>>(ReadJsonFile(TRISTIMULUS_WEIGHTING_WEIGHTING_FACTORS_5NM_TABLE_File))
            };
            TRISTIMULUS_WEIGHTING_WEIGHTING_FACTORS_10NM_TABLE = new TristimulusWeightingFactorTable()
            {
                _factors = JsonConvert.DeserializeObject<List<TristimulusWeightingFactor>>(ReadJsonFile(TRISTIMULUS_WEIGHTING_WEIGHTING_FACTORS_10NM_TABLE_File))
            };
        }
        private List<TristimulusWeightingFactor> _factors = new List<TristimulusWeightingFactor>();
        public static TristimulusWeightingFactorTable TRISTIMULUS_WEIGHTING_WEIGHTING_FACTORS_5NM_TABLE { get; private set; }

        public static TristimulusWeightingFactorTable TRISTIMULUS_WEIGHTING_WEIGHTING_FACTORS_10NM_TABLE { get; private set; }

        public TristimulusWeightingFactor this[StandardObserver observer, StandardIlluminant illuminant] => _factors.FirstOrDefault(x => x.Observer == observer && x.Illuminant == illuminant);
        private static string ReadJsonFile(string fileName)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            fileName = $@"{assembly.GetName().Name}.Resources.{fileName}";
            using (var stream = assembly.GetManifestResourceStream(fileName))
            {
                var data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                return System.Text.Encoding.ASCII.GetString(data);
            }
        }

        /// <summary>
        /// 根据光源，观察者，波长获取三色权重因子
        /// </summary>
        /// <param name="illuminant"></param>
        /// <param name="observer"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static Tuple<float[], float[], float[]> GetTristimulusValues(StandardIlluminant illuminant, StandardObserver observer, int interval)
        {
            var datas = 5 / interval == 1 ?
                TRISTIMULUS_WEIGHTING_WEIGHTING_FACTORS_5NM_TABLE[observer, illuminant].Factors :
                TRISTIMULUS_WEIGHTING_WEIGHTING_FACTORS_10NM_TABLE[observer, illuminant].Factors;
            List<float> tmpX = new List<float>();
            List<float> tmpY = new List<float>();
            List<float> tmpZ = new List<float>();
            for (int waveLength = 360; waveLength <= 780; waveLength += interval)
            {
                var index = (waveLength - 360) / interval;
                tmpX.Add(datas[index, 0]);
                tmpY.Add(datas[index, 1]);
                tmpZ.Add(datas[index, 2]);
            }
            return new Tuple<float[], float[], float[]>(tmpX.ToArray(), tmpY.ToArray(), tmpZ.ToArray());
        }
    }
}
