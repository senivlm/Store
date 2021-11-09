using System;
using System.Text.RegularExpressions;

namespace Store
{
    public enum QualityGrade { Highest, First, Second }

    public enum MeatType { Mutton, Beef, Pork, Chicken }

    public class Meat : Product, IEquatable<Meat>
    {
        public readonly QualityGrade Quality;
        public readonly MeatType Type;

        public Meat(string name, double price, double weight, int consumeIn, QualityGrade quality, MeatType type)
            : this(name, price, weight, consumeIn, DateTime.Now, quality, type)
        { }

        public Meat(string name, double price, double weight, int consumeIn, DateTime productionDate, QualityGrade quality, MeatType type)
            : base(name, price, weight, consumeIn, productionDate)
        {
            Quality = quality; Type = type;
        }

        public Meat() : this("ProductName", 0, 0, 0, 0, 0)
        {
        }

        public new static Meat Parse(string s)
        {
            var match = Regex.Match(s,
                @"(\w+), \$(\d+(?:.\d+)?), (\d+(?:.\d+)?)kg, consume in (\d+) days, produced on (\d{2}/\d{2}/\d{4})" +
                ", quality: (Highest|First|Second), type: (Mutton|Beef|Pork|Chicken)");
            if (!match.Success) throw new FormatException("Format seems to be wrong");

            var groups = match.Groups;
            return new Meat(
                groups[1].Value,
                double.Parse(groups[2].Value),
                double.Parse(groups[3].Value),
                int.Parse(groups[4].Value),
                DateTime.Parse(groups[5].Value),
                Enum.Parse<QualityGrade>(groups[6].Value),
                Enum.Parse<MeatType>(groups[7].Value)
            );
        }

        public static bool TryParse(string s, out Meat pr)
        {
            try
            {
                pr = Parse(s);
                return true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            pr = new Meat();
            return false;
        }

        public static bool operator ==(Meat pr1, Meat pr2) => pr1.Equals(pr2);

        public static bool operator !=(Meat pr1, Meat pr2) => !pr1.Equals(pr2);

        public override void MultPrice(double multiplier) =>
            base.MultPrice(0.95 * multiplier - 0.05 * (int)Quality);

        public bool Equals(Meat pr) =>
            base.Equals(pr) && Quality == pr.Quality && Type == pr.Type;

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;
            return base.Equals(obj as Meat);
        }

        public override int GetHashCode() => ToString().GetHashCode();

        public override string ToString() =>
            $"{base.ToString()}, quality: {Quality}, type: {Type}";
    }
}
