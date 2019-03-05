using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Properties;
using MathNet.Numerics.Statistics;

namespace BookShop.Common
{
    /// <summary>2个数据集的相关度计算类</summary>
    public class Correlation
    {
        /// <summary>计算皮尔逊积差相关系数</summary>
        /// <param name="dataA">数据样本A.</param>
        /// <param name="dataB">数据样本B.</param>
        /// <returns>返回皮尔逊积差相关系数.</returns>
        public static double Pearson(IEnumerable<double> dataA, IEnumerable<double> dataB)
        {
            int n = 0;
            double r = 0.0;
            double meanA = 0;
            double meanB = 0;
            double varA = 0;
            double varB = 0;

            using (IEnumerator<double> ieA = dataA.GetEnumerator())
            using (IEnumerator<double> ieB = dataB.GetEnumerator())
            {
                while (ieA.MoveNext())
                {
                    if (!ieB.MoveNext())
                    {
                        throw new ArgumentOutOfRangeException("dataB", Resources.ArgumentArraysSameLength);
                    }
                    double currentA = ieA.Current;
                    double currentB = ieB.Current;
                    double deltaA = currentA - meanA;
                    double scaleDeltaA = deltaA / ++n;
                    double deltaB = currentB - meanB;
                    double scaleDeltaB = deltaB / n;
                    meanA += scaleDeltaA;
                    meanB += scaleDeltaB;
                    varA += scaleDeltaA * deltaA * (n - 1);
                    varB += scaleDeltaB * deltaB * (n - 1);
                    r += (deltaA * deltaB * (n - 1)) / n;
                }
                if (ieB.MoveNext())
                {
                    throw new ArgumentOutOfRangeException("dataA", Resources.ArgumentArraysSameLength);
                }
            }
            return r / Math.Sqrt(varA * varB);
        }
        /// <summary>计算加权皮尔逊积差相关系数.</summary>
        /// <param name="dataA">数据样本A.</param>
        /// <param name="dataB">数据样本B.</param>
        /// <param name="weights">数据权重.</param>
        /// <returns>加权皮尔逊积差相关系数.</returns>
        public static double WeightedPearson(IEnumerable<double> dataA, IEnumerable<double> dataB, IEnumerable<double> weights)
        {
            int n = 0;
            double meanA = 0;
            double meanB = 0;
            double varA = 0;
            double varB = 0;
            double sumWeight = 0;
            double covariance = 0;
            using (IEnumerator<double> ieA = dataA.GetEnumerator())
            using (IEnumerator<double> ieB = dataB.GetEnumerator())
            using (IEnumerator<double> ieW = weights.GetEnumerator())
            {
                while (ieA.MoveNext())
                {
                    if (!ieB.MoveNext())
                    {
                        throw new ArgumentOutOfRangeException("dataB", Resources.ArgumentArraysSameLength);
                    }
                    if (!ieW.MoveNext())
                    {
                        throw new ArgumentOutOfRangeException("weights", Resources.ArgumentArraysSameLength);
                    }
                    ++n;
                    double xi = ieA.Current;
                    double yi = ieB.Current;
                    double wi = ieW.Current;
                    double temp = sumWeight + wi;
                    double deltaX = xi - meanA;
                    double rX = deltaX * wi / temp;
                    meanA += rX;
                    varA += sumWeight * deltaX * rX;
                    double deltaY = yi - meanB;
                    double rY = deltaY * wi / temp;
                    meanB += rY;
                    varB += sumWeight * deltaY * rY;
                    sumWeight = temp;
                    covariance += deltaX * deltaY * (n - 1) * wi / n;
                }
                if (ieB.MoveNext())
                {
                    throw new ArgumentOutOfRangeException("dataB", Resources.ArgumentArraysSameLength);
                }
                if (ieW.MoveNext())
                {
                    throw new ArgumentOutOfRangeException("weights", Resources.ArgumentArraysSameLength);
                }
            }
            return covariance / Math.Sqrt(varA * varB);
        }
        /// <summary>计算皮尔逊积差相关矩阵</summary>
        /// <param name="vectors">数据矩阵</param>
        /// <returns>皮尔逊积差相关矩阵.</returns>
        public static Matrix<double> PearsonMatrix(params double[][] vectors)
        {
            var m = Matrix<double>.Build.DenseIdentity(vectors.Length);
            for (int i = 0; i < vectors.Length; i++)
            {
                for (int j = i + 1; j < vectors.Length; j++)
                {
                    var c = Pearson(vectors[i], vectors[j]);
                    m.At(i, j, c);
                    m.At(j, i, c);
                }
            }
            return m;
        }
        /// <summary> 计算皮尔逊积差相关矩阵</summary>
        /// <param name="vectors">数据集合.</param>
        /// <returns>皮尔逊积差相关矩阵.</returns>
        public static Matrix<double> PearsonMatrix(IEnumerable<double[]> vectors)
        {
            return PearsonMatrix(vectors as double[][] ?? vectors.ToArray());
        }
        /// <summary>
        /// 斯皮尔曼等级相关系数
        /// </summary>
        /// <param name="dataA">数据集A.</param>
        /// <param name="dataB">数据集B.</param>
        /// <returns>斯皮尔曼等级相关系数.</returns>
        public static double Spearman(IEnumerable<double> dataA, IEnumerable<double> dataB)
        {
            return Pearson(Rank(dataA), Rank(dataB));
        }
        /// <summary>
        /// 斯皮尔曼等级相关矩阵
        /// Computes the Spearman Ranked Correlation matrix.
        /// </summary>
        /// <param name="vectors">数据集.</param>
        /// <returns>斯皮尔曼等级相关矩阵.</returns>
        public static Matrix<double> SpearmanMatrix(params double[][] vectors)
        {
            return PearsonMatrix(vectors.Select(Rank).ToArray());
        }
        /// <summary>计算斯皮尔曼等级相关矩阵</summary>
        /// <param name="vectors">数据集合.</param>
        /// <returns>斯皮尔曼等级相关矩阵.</returns>
        public static Matrix<double> SpearmanMatrix(IEnumerable<double[]> vectors)
        {
            return PearsonMatrix(vectors.Select(Rank).ToArray());
        }
        static double[] Rank(IEnumerable<double> series)
        {
            if (series == null)
            {
                return new double[0];
            }
            // WARNING: do not try to cast series to an array and use it directly,
            // as we need to sort it (inplace operation)
            var data = series.ToArray();
            return ArrayStatistics.RanksInplace(data, RankDefinition.Average);
        }

        //物品推荐
        public static Dictionary<string, List<Recommendation>> productRecommendations =
           new Dictionary<string, List<Recommendation>>();

        public static double CalculatePearsonCorrelation(string product1, string product2)
        {
            List<Recommendation> shared_items = new List<Recommendation>();

            // collect a list of products have have reviews in common
            foreach (var item in productRecommendations[product1])
            {
                if (productRecommendations[product2].Where(x => x.Name == item.Name).Count() != 0)
                {
                    shared_items.Add(item);
                }
            }

            if (shared_items.Count == 0)
            {
                // they have nothing in common exit with a zero
                return 0;
            }

            // sum up all the preferences
            double product1_review_sum = 0.00f;
            foreach (Recommendation item in shared_items)
            {
                product1_review_sum += productRecommendations[product1].Where(
                         x => x.Name == item.Name).FirstOrDefault().Rating;
            }

            double product2_review_sum = 0.00f;
            foreach (Recommendation item in shared_items)
            {
                product2_review_sum += productRecommendations[product2].Where(
                         x => x.Name == item.Name).FirstOrDefault().Rating;
            }

            // sum up the squares
            double product1_rating = 0f;
            double product2_rating = 0f;
            foreach (Recommendation item in shared_items)
            {
                product1_rating += Math.Pow(productRecommendations[product1].Where(
                         x => x.Name == item.Name).FirstOrDefault().Rating, 2);

                product2_rating += Math.Pow(productRecommendations[product2].Where(
                         x => x.Name == item.Name).FirstOrDefault().Rating, 2);
            }

            //sum up the products
            double critics_sum = 0f;
            foreach (Recommendation item in shared_items)
            {
                critics_sum += productRecommendations[product1].Where(
                        x => x.Name == item.Name).FirstOrDefault().Rating *
                productRecommendations[product2].Where(
                       x => x.Name == item.Name).FirstOrDefault().Rating;
            }

            //calculate pearson score
            double num = critics_sum - (product1_review_sum *
              product2_review_sum / shared_items.Count);

            double density = (double)Math.Sqrt((product1_rating -
              Math.Pow(product1_review_sum, 2) / shared_items.Count) *
              ((product2_rating - Math.Pow(product2_review_sum, 2) / shared_items.Count)));

            if (density == 0)
                return 0;

            return num / density;
        }

        public static List<Recommendation> getTest(string name)
        {
            // grab of list of products that *excludes* the item we're searching for
            var sortedList = productRecommendations.Where(x => x.Key != name);

            sortedList.OrderByDescending(x => x.Key);

            List<Recommendation> recommendations = new List<Recommendation>();

            // go through the list and calculate the Pearson score for each product
            foreach (var entry in sortedList)
            {
                recommendations.Add(new Recommendation()
                {
                    Name = entry.Key,
                    Rating = CalculatePearsonCorrelation(name, entry.Key)
                });
            }

            return recommendations;
        }
    }

    public class Recommendation
    {
        public string Name { get; set; }
        public double Rating { get; set; }
    }
}
