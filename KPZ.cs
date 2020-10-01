using System;
using System.Reflection.Metadata.Ecma335;

namespace Pract1
{
    class Program
    {
        private
            const int n = 3;

        public
            static Entrant[] ReadEntrantsArray() 
        {
            Entrant[] ents = new Entrant[n];
            for (int i = 0; i < n; i++) {
                Console.WriteLine("Name: ");
                    ents[i].Name = Console.ReadLine();
                Console.WriteLine("Id: ");
                    ents[i].IdNum = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Course points: ");
                    ents[i].CoursePoints = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Average points: ");
                    ents[i].AvgPoints = Convert.ToSingle(Console.ReadLine());

                ZNO[] znoArr = new ZNO[3];
                for (int j = 0; j < 3; j++) {
                    Console.WriteLine($"ZNO subject {j + 1}: ");
                        znoArr[j].Subject = Console.ReadLine();
                    Console.WriteLine($"ZNO point {j + 1}: ");
                        znoArr[j].Points = Convert.ToInt32(Console.ReadLine());
                }
                ents[i].ZNOResults = znoArr;

                Console.WriteLine("");      
            }
            return ents;
        }

        public static void PrintEntrant(Entrant entrant) {
            Console.WriteLine("Name: ");
            Console.WriteLine(entrant.Name);
            Console.WriteLine("Id: ");
            Console.WriteLine(entrant.IdNum);
            Console.WriteLine("Course points: ");
            Console.WriteLine(entrant.CoursePoints);
            Console.WriteLine("Average points: ");
            Console.WriteLine(entrant.AvgPoints);

            for (int i = 0; i < 3; i++) {
                Console.WriteLine($"ZNO point {i + 1}: ");
                Console.WriteLine(entrant.ZNOResults[i].Points);
            }
        }

        public static void PrintEntrants(Entrant[] entrants) {
            for (int i = 0; i < n; i++) {
                PrintEntrant(entrants[i]);
            }
        }

        public static void GetEntrantsInfo(Entrant[] ents, out Entrant min, out Entrant max)
        {
            Entrant mn = ents[0], mx = ents[0];
            for (int i = 1; i < ents.Length; i++)
            {
                if (ents[i].GetCompMark() < mn.GetCompMark()){  mn = ents[i];   }
                
                if (ents[i].GetCompMark() > mx.GetCompMark()){  mx = ents[i];   }
            }
            min = mn;
            max = mx;
        }

        public static void Swap(ref Entrant a, ref Entrant b) {
            Entrant temp = a;
            a = b;
            b = temp;
        }

        public static void SortEntrantsByPoints(ref Entrant[] ents)
        {
            for (int i = 1; i < ents.Length; i++) {
                for (int j = 0; j < ents.Length - 1; j++) {
                    if (ents[j].GetCompMark() < ents[j + 1].GetCompMark()) 
                    {
                        Swap(ref ents[j], ref ents[j + 1]);
                    }
                }
            }
        }

        public static void SortEntrantsByName(ref Entrant[] ents)
        {
            for (int i = 0; i < ents.Length; i++)
            {
                for (int j = 0; j < ents.Length - 1; j++)
                {
                    if (NeedToReOrder(ents[j].Name, ents[j + 1].Name))
                    {
                        Entrant temp = ents[j];
                        ents[j] = ents[j + 1];
                        ents[j + 1] = temp;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Entrant[] ents = ReadEntrantsArray();
            SortEntrantsByName(ref ents);

            PrintEntrants(ents);
        }

    public static bool NeedToReOrder(string s1, string s2)
        {
            for (int i = 0; i < (s1.Length > s2.Length ? s2.Length : s1.Length); i++)
            {
                if (s1.ToCharArray()[i] < s2.ToCharArray()[i]) return false;
                if (s1.ToCharArray()[i] > s2.ToCharArray()[i]) return true;
            }
            return false;
        }
    }

    struct Entrant
    {
        public string Name;
        public int IdNum;
        public double CoursePoints;
        public double AvgPoints;
        public ZNO[] ZNOResults;

        public double GetCompMark() {
            if (ZNOResults.Length < 3) {
                return 0;
            } 
            
            return CoursePoints * 0.05 + AvgPoints * 0.10 + ZNOResults[0].Points * 0.25 + ZNOResults[1].Points * 0.4 + ZNOResults[2].Points * 0.2;
        }

        public string GetBestSubject() {
            ZNO best = ZNOResults[0];
            for (int i = 1; i < 3; i++) {
                if (ZNOResults[i].Points > best.Points) {
                    best = ZNOResults[i];
                }
            }

            return best.Subject;
        }
        
        public string GetWorstSubject() {
            ZNO worst = ZNOResults[0];
            for (int i = 1; i < 3; i++) {
                if (ZNOResults[i].Points < worst.Points) {
                    worst = ZNOResults[i];
                }
            }

            return worst.Subject;
        }

        public Entrant(string Name, int IdNum, float CoursePoints, float AvgPoints, ZNO[] ZNOResults) 
        {
            this.Name = Name;
            this.IdNum = IdNum;
            this.CoursePoints = CoursePoints;
            this.AvgPoints = AvgPoints;
            this.ZNOResults = ZNOResults;
        }
    }

    struct ZNO 
    {
        public string Subject;
        public int Points;

        public ZNO(string Subject, int Points) 
        {
            this.Subject = Subject;
            this.Points = Points;
        }
    }
}