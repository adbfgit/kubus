using System;
using System.Diagnostics;

namespace kubus
{
    //dit programma berekent hoeveel blokjes je weg kunt halen uit een vorm met 32 blokjes ( alle ribben van een 4x4x4 kubus ), zodat de projecties allemaal vierkanten zijn, dus er nog hetzelfde uitzien.
    //het antwoord is 12, dit programma berekent hoe de vorm er uit ziet.
    class Program
    {
        int[] matrix = new int[31];                                                     //array met plek voor alle 31 blokjes

        static (int[,,] resultmatrix, bool found) CheckMatrix(int[]matrix)              //methode checked of de volgende iteratie een geldige oplossing geeft. Methode neemt "matrix" en output is de 3dMatrix en een bool dat eengeldige oplossing is gevonden.
        {
        
            int[,,] dMatrix = new int[4, 4, 4];                                         // de 3d Matrix, wordt hieronder gevuld met de verdeling van 12 blokjes (1) en 20 lege plekken (0) van de 32.
                                  
            dMatrix[0, 0, 0] = matrix[0];                      
            dMatrix[1, 0, 0] = matrix[1];
            dMatrix[2, 0, 0] = matrix[2];
            dMatrix[3, 0, 0] = matrix[3];
            dMatrix[0, 1, 0] = matrix[4];
            dMatrix[3, 1, 0] = matrix[5];
            dMatrix[0, 2, 0] = matrix[6];
            dMatrix[3, 2, 0] = matrix[7];
            dMatrix[0, 3, 0] = matrix[8];
            dMatrix[1, 3, 0] = matrix[9];
            dMatrix[2, 3, 0] = matrix[10];
            dMatrix[3, 3, 0] = matrix[11];
            
            dMatrix[0, 0, 1] = matrix[12];
            dMatrix[3, 0, 1] = matrix[13];
            dMatrix[0, 3, 1] = matrix[14];
            dMatrix[3, 3, 1] = matrix[15];

            dMatrix[0, 0, 2] = matrix[16];
            dMatrix[3, 0, 2] = matrix[17];
            dMatrix[0, 3, 2] = matrix[18];
            dMatrix[3, 3, 2] = matrix[19];

            dMatrix[0, 0, 3] = matrix[20];
            dMatrix[1, 0, 3] = matrix[21];
            dMatrix[2, 0, 3] = matrix[22];
            dMatrix[3, 0, 3] = matrix[23];
            dMatrix[0, 1, 3] = matrix[24];
            dMatrix[3, 1, 3] = matrix[25];
            dMatrix[0, 2, 3] = matrix[26];
            dMatrix[3, 2, 3] = matrix[27];
            dMatrix[0, 3, 3] = matrix[28];
            dMatrix[1, 3, 3] = matrix[29];
            dMatrix[2, 3, 3] = matrix[30];
            dMatrix[3, 3, 3] = matrix[31];

            int[,] zWaarden = new int[4, 4];                                    //de 2dmatrices die de projecties weergeven 
            int[,] yWaarden = new int[4, 4];
            int[,] xWaarden = new int[4, 4];

            bool check = true;

            if (check)
            {
                for (int x = 0; x < 4; x++)                                     //check of de z-projectie ( dus van boven ) volledig dicht is. Als er een 0 in voorkomt dan false, oplossing bestaat niet.
                {
                    if (check)
                    {
                        for (int y = 0; y < 4; y++)
                        {
                            if (check && x * y != 1 && x * y != 2 && x * y != 4)
                            {
                                for (int z = 0; z < 4; z++)
                                {
                                    zWaarden[x, y] = zWaarden[x, y] + dMatrix[x, y, z];
                                }
                                if (zWaarden[x, y] == 0) { check = false; x = 4; y = 4; }
                            }
                        }
                    }
                }
            }

            if (check)
            {
                for (int x = 0; x < 4; x++)                                     //check de y-projectie.
                {
                    if (check)
                    {
                        for (int z = 0; z < 4; z++)
                        {
                            if (check && x * z != 1 && x * z != 2 && x * z != 4)
                            {
                                for (int y = 0; y < 4; y++)
                                    {
                                        yWaarden[x, z] = yWaarden[x, z] + dMatrix[x, y, z];
                                    }
                                if (yWaarden[x, z] == 0) { check = false; x = 4; z = 4; }
                            }
                        }
                    }
                }
            }

            if (check)
            {
                for (int z = 0; z < 4; z++)                                     //check de x-projectie.
                {
                    if (check)
                    {
                        for (int y = 0; y < 4; y++)
                        {
                            if (check && z * y != 1 && z * y != 2 && z * y != 4)
                            {
                                for (int x = 0; x < 4; x++)
                                {
                                    xWaarden[z, y] = xWaarden[z, y] + dMatrix[x, y, z];
                                }
                                if (xWaarden[z, y] == 0) { check = false; y = 4; z = 4; }
                            }
                        }
                    }
                }
            }
            
            if (check)                                                          // als er een geldige oplossing is dan wordt de 3d matrix terug gegeven en een true, anders een false.
            {
                return (dMatrix,true);
            }
            else 
            {
                int[,,] emptyMatrix = new int[0, 0, 0];
                return (emptyMatrix,false);  
            }
        }

static void GeneratePermutations(int length, int dots)                          //Methode loopt alle mogelijke combinaties van 12 blokjes ("dots") over 32 plaatsen ("length") langs.
        {
            
            int[] matrix = new int[length];                                     // init matrix 32
            
            if (dots>length)                                                    // check of de invoer goed is.
            {
                Console.WriteLine("blocks>lenght");
                return;
            }
            
            for (int i = length -1; i > length - dots-1; i--)                   //vul de matrix met 12 ene ( aan het eind)
            {
                matrix[i] = 1;
            }

            
            int DotsLeftOfPosition(int pos)                                     //tel het aantal blokjes links van een positie in de matrix.
            {
                int numberOfDots = 0;

                for (int i = 0; i < pos-1; i++)
                {
                    numberOfDots = numberOfDots + matrix[i];
                }
                return numberOfDots;  
            }

            void PrintMatrix()                                                  //print de matrix
            {
                for (int i = 0; i < length; i++)
                {
                    Console.Write(matrix[i] + "");
                }
                Console.WriteLine();
            }

            bool IsMatrixReady()                                                //check of het proces klaar is, dat is als de eerste 12 plaatsen allemaal een 1 zijn.
            {
                int sumDots = 0;
                for (int i = 0; i < dots; i++)
                {
                    sumDots= sumDots+matrix[i];
                }
                return sumDots == dots;  
            }

            PrintMatrix();

            int pos = 1;
            int teller = 0;
            
            do                                                                  //loop de matrix door alle permutaties heen.
            {
                if (matrix[pos - 1] == 0 && matrix[pos] == 1)                   //als een blokje 1 naar links kan schuiven
                {
                    matrix[pos - 1] = 1; matrix[pos] = 0;                       //schuif blokje naar links.

                    int dotsLeftOfPosition = DotsLeftOfPosition(pos);           //deze 2 regels en de 2 for next loops laten alle blokjes links van het net verschoven blokje aansluiten op dat blokje 

                    int firstNewDotPosAfterSwipe = pos - dotsLeftOfPosition - 1;

                    for (int i = 0; i < pos - 1; i++)
                    {
                        matrix[i] = 0;
                    }

                    for (int i = firstNewDotPosAfterSwipe; i <= dotsLeftOfPosition + firstNewDotPosAfterSwipe; i++)
                    {
                        matrix[i] = 1;
                    }
                    
                    teller++;
                    
                    if (teller%10000000==0)                                     //print iedere 10milioenste doorloop.
                    {
                        Console.Write("{0:n0}: ",teller );
                        PrintMatrix();
                    }


                     pos = 1;
                     if(CheckMatrix(matrix).found)                              //heeft de itaratie een oplossinggevonden?
                     {   
                            PrintMatrix(); 
                            Console.WriteLine("found: ");

                        for (int z = 0; z < 4; z++)                             //print oplossing voor de verscillende niveaus van z.
                        {
                            Console.WriteLine("z = " + z);
                            for (int x = 0; x < 4; x++)
                            {
                                for (int y = 0; y < 4; y++)
                                {
                                    Console.Write(" " + CheckMatrix(matrix).resultmatrix[x, y, z]);   
                                }
                                Console.WriteLine();
                            }
                        }
                     }
                }
                else if (pos < length - 1)                                      //als een blokje niet naar links kan schuiven dan check of het volgende blokje rechts daarvan naar links kan schuiven
                {
                    pos++;
                }

            } while (!IsMatrixReady());
                                                  
           PrintMatrix();
            
        }
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            GeneratePermutations(32, 12);
            
            stopwatch.Stop();
            
            Console.WriteLine();
            Console.WriteLine("Time elapsed: " + stopwatch.Elapsed);
            Console.ReadLine();
        }
    }
}
