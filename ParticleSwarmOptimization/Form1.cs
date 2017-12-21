using System;
using System.Windows.Forms;

namespace ParticleSwarmOptimization
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ParticleProgram.Run(GetError);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ParticleProgram.Run(GetError2, particleCount: 5, dimensions: 1, minAcceptedError: 0.00000001);
        }

        static double GetError(double[] x)
        {
            // 0.42888194248035300000 when x0 = -sqrt(2), x1 = 0
            double expectedMin = -0.42888194; // true min for z = x * exp(-(x^2 + y^2))
            double z = x[0] * Math.Exp(-((x[0] * x[0]) + (x[1] * x[1])));
            return Math.Pow(z - expectedMin, 2); // MSE
        }

        static double GetError2(double[] x)
        {
            // -ax\ -\ \ b ^ 2\ +\ 2 ^ x
            double expectedMin = -0.0861; // true min x,y = (0.5288, -0.0861)
            double z = -x[0] - 1 + Math.Pow(2, x[0]);
            return Math.Pow(z - expectedMin, 2); // MSE
        }

        /// <summary>
        /// Returns the Mean Square error for a solution to Himmelblau's function.
        /// <remarks>Min z = 0. Many global minima x,y = { (3, 2), (-2.805, -3.28), (-3.779, -3.283), (3.584, -1.848) }.
        /// Wiki info: https://en.wikipedia.org/wiki/Himmelblau%27s_function. </remarks>
        /// </summary>
        /// <param name="xArray">Proposed solution 2d array for input values.</param>
        /// <returns>Mean Squared Error, double value.</returns>
        static double HimmelblauMse(double[] xArray)
        {
            double expectedMin = 0; // Min z = 0. Many global minima x,y = { (3, 2), (-2.805, -3.28), (-3.779, -3.283), (3.584, -1.848) }
            double x = xArray[0];
            double y = xArray[1];
            double z = Math.Pow(Math.Pow(x, 2) + y - 11, 2) + Math.Pow(x + Math.Pow(y, 2) - 7, 2);
            return Math.Pow(z - expectedMin, 2); // MSE
        }

        /// <summary>
        /// Returns the Mean Squared Error for a solution to Michalewicz's function for 2 dimensions. 
        /// <remarks>Has factorial dimension (d!) local minima. Parameter m (default: 10), defines the steepness of the minima.
        /// For 2 dimensions, min z = -1.8013, global minima: x,y = (2.20, 1.57) with x_i in (0, PI).
        /// More info: https://www.sfu.ca/~ssurjano/michal.html </remarks>
        /// </summary>
        /// <param name="xArray">Proposed solution 2d array for input values.</param>
        /// <returns>Mean Squared Error, double value.</returns>
        static double MichalewiczMse2(double[] xArray)
        {
            int d = 2; // Dimensions
            double expectedMin = -1.8013; // For 2 dimensions, min z = -1.8013. Global minima x,y = (2.20, 1.57) with x_i in (0, PI)
            int m = 10; // constant with default value 10

            double sum = 0;
            for (int i = 1; i <= d; i++)
            {

                double xi = xArray[i - 1];
                sum += Math.Sin(xi) * Math.Pow(Math.Sin(i * Math.Pow(xi, 2) / Math.PI), 2 * m);
            }
            double z = -sum;

            return Math.Pow(z - expectedMin, 2); // MSE
        }

        static double MichalewiczMse5(double[] xArray)
        {
            int d = 5; // Dimensions
            double expectedMin = -4.687658; // For 5 dimensions, min z = -4.687658 with x_i in (0, PI)
            int m = 10; // constant with default value 10

            double sum = 0;
            for (int i = 1; i <= d; i++)
            {

                double xi = xArray[i - 1];
                sum += Math.Sin(xi) * Math.Pow(Math.Sin(i * Math.Pow(xi, 2) / Math.PI), 2 * m);
            }
            double z = -sum;

            return Math.Pow(z - expectedMin, 2); // MSE
        }
        
        static double EggHolder(double[] xArray)
        {
            // See: https://www.sfu.ca/~ssurjano/egg.html
            double expectedMin = -959.6407; // Min z = -959.6407, with x,y = (512, 404.2319) in [-512, 512]
            double x = xArray[0];
            double y = xArray[1];
            double z = -(y + 47) * Math.Sin(Math.Sqrt(Math.Abs(y + x / 2 + 47))) - x * Math.Sin(Math.Sqrt(Math.Abs(x - (y + 47))));
            return Math.Pow(z - expectedMin, 2); // MSE
        }

        static double MishraSBird(double[] xArray)
        {
            // See: https://en.wikipedia.org/wiki/Test_functions_for_optimization
            double expectedMin = -106.7645367; // Global Min z = -106.7645367 with x,y = (-3.31302468, -1.5821422). Search domain: x in [-10, 0] and y in [-6.5, 0]
            double x = xArray[0];
            double y = xArray[1];

            if (x < -10 || y < -6.5 || x > 0 || y > 0) // Search domain constraints: x in [-10, 0] and y in [-6.5, 0]
                return double.MaxValue;

            double subjectedTo = Math.Pow(x + 5, 2) + Math.Pow(y + 5, 2);
            double lessThanValue = 25;
            if (subjectedTo >= lessThanValue) // Subjected to constraint
                return double.MaxValue;
            
            double z = Math.Sin(y) * Math.Pow(Math.E, Math.Pow(1 - Math.Cos(x), 2)) + Math.Cos(x) * Math.Pow(Math.E, Math.Pow(1 - Math.Sin(y), 2)) + Math.Pow(x - y, 2);
            return Math.Pow(z - expectedMin, 2); // MSE
        }

        static double Townsend(double[] xArray)
        {
            // See: https://en.wikipedia.org/wiki/Test_functions_for_optimization
            double expectedMin = -2.0239884; // Global Min z = -2.0239884 with x,y = (2.0052938, 1.1944509). Search domain: x in [-2.25, 2.5] and y in [-2.5, 1.75]
            double x = xArray[0];
            double y = xArray[1];

            if (x < -2.25 || y < -2.5 || x > 2.5 || y > 1.75) // Search domain constraints: x in [-2.25, 2.5] and y in [-2.5, 1.75]
                return double.MaxValue;

            double subjectedTo = Math.Pow(x, 2) + Math.Pow(y, 2);
            double t = Math.Atan2(x, y);

            double lessThanValue = Math.Pow(
                2 * Math.Cos(t) - (1 / (double)2) * Math.Cos(2 * t) - (1/(double)4) * Math.Cos(3 * t) - (1/(double)8) * Math.Cos(4 * t)
                , 2) + Math.Pow(2 * Math.Sin(t), 2);
            if (subjectedTo >= lessThanValue) // Subjected to constraint
                return double.MaxValue;
            
            double z = -Math.Pow(Math.Cos((x - 0.1) * y) , 2) - x * Math.Sin(3 * x + y);
            return Math.Pow(z - expectedMin, 2); // MSE
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ParticleProgram.Run(HimmelblauMse, particleCount: 1000, dimensions: 2, maxEpochs:30, minAcceptedError: 0.00000001);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ParticleProgram.Run(MichalewiczMse2, particleCount: 1000, dimensions: 2, maxEpochs: 30, minAcceptedError: 0, minX: 0, maxX: Math.PI);
            //ParticleProgram.Run(MichalewiczMse5, particleCount: 1000, dimensions: 5, maxEpochs: 1000, minAcceptedError: 0, minX: 0, maxX: Math.PI);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ParticleProgram.Run(EggHolder, particleCount: 1000, dimensions: 2, maxEpochs: 30, minAcceptedError: 0, minX: -512, maxX: 512);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ParticleProgram.Run(MishraSBird, particleCount: 1000, dimensions: 2, maxEpochs: 30, minAcceptedError: 0, minX: -10, maxX: 0);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ParticleProgram.Run(Townsend, particleCount: 1000, dimensions: 2, maxEpochs: 300, minAcceptedError: 0, minX: -2.5, maxX: 2.5);
        }
    }
}
