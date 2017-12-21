using System.Linq;

namespace ParticleSwarmOptimization
{
    public class Particle
    {
        public double[] Position;
        public double[] Velocity;
        public double[] BestPosition;
        public double Error;
        public double BestError;

        public Particle(double[] position, double error, double[] velocity, double[] bestPosition, double bestError)
        {
            Position = new double[position.Length];
            position.CopyTo(Position, 0);

            Error = error;
            Velocity = new double[velocity.Length];
            velocity.CopyTo(Velocity, 0);

            BestPosition = new double[bestPosition.Length];
            bestPosition.CopyTo(BestPosition, 0);
            BestError = bestError;
        }

        public override string ToString()
        {
            string s = "==========================\nPosition: ";
            s = Position.Aggregate(s, (current, t) => current + (t.ToString("F4") + " "));
            s += "\n";
            s += "Error = " + Error.ToString("F4") + "\n";
            s += "Velocity: ";
            s = Velocity.Aggregate(s, (current, t) => current + (t.ToString("F4") + " "));
            s += "\n";
            s += "Best Position: ";
            s = BestPosition.Aggregate(s, (current, t) => current + (t.ToString("F4") + " "));
            s += "\n";
            s += "Best Error = " + BestError.ToString("F4") + "\n";
            s += "==========================\n";
            return s;
        }
    }
}
