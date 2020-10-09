using System;


namespace function
{
    class Program

    { public static int firstClose = 0, pastOpen = 0;
        enum TrigonometricFunction 
        {
            sin,cos,ln,tg,ctg,x
        }
        public static string Check(string y)
        {
            int i = 0;

            while (y[i] != '=')
            {
                i++;
                if (i==y.Length-1)
                {
                    Console.WriteLine("Неправельно задана функция");
                    break;                   
                }
            }
                      
            y = '('+y.Substring(i+1, y.Length-i-1)+')';
            
                return y;
        }
        public static string Decoupling(string y)
        {
            for (int i = 0; i < y.Length; i++)
            {
                if (y[i] == '*' || y[i] == '/')
                {
                    if(y[i-1]==')')
                    {
                        y ='('+ y.Substring(0, i - 1)  + y.Substring(i - 1, 3) + ")" + y.Substring(i + 2);
                        i++;
                    }
                    else if(y[i+1]=='(')
                    {                        
                        y = y.Substring(0, i - 1) + "(" + y.Substring(i - 1) + ")" ;
                        i++;
                    }
                    else
                    {
                        y = y.Substring(0, i - 1) + "(" + y.Substring(i - 1, 3) + ")" + y.Substring(i + 2);
                        i++;
                    }
                    
                }
                    
            }
            return y;
        }
        public static string Simplification(string y)
        {
            
            for (int i = 0; i < y.Length; i++)
            {
                if (y[i] == '(')
                    pastOpen = i+1;

                if (y[i] == ')')
                {
                    firstClose = i;
                    break;
                }
            }
            
            y = y.Substring(pastOpen, firstClose-pastOpen);
            return y;
        }
        public static int Сounting(string y,int x)
        {
            string ycopy, maths,yleft,yright;
            char znak=' ';            
            int i = 0, result=0, numb,j=0;
            
            while(y!="")   
            {
                for (int count = 0; count < y.Length; count++)
                {
                    if(y[count]=='(')
                    {
                        ycopy = Simplification(y)+' ';                            
                        while(ycopy!="")
                        {
                            i = 0;
                            if (Int32.TryParse(Convert.ToString(ycopy[i]), out numb))
                            {
                                j = i;
                                while (Int32.TryParse(Convert.ToString(ycopy[i]), out numb))
                                    i++;

                                numb = Convert.ToInt32(ycopy.Substring(j, i));
                                ycopy = ycopy.Substring(i);

                            }
                            else if (ycopy[i] == '+' || ycopy[i] == '-' || ycopy[i] == '*' || ycopy[i] == '/')
                            {
                                znak = ycopy[i];
                                i++;
                                ycopy = ycopy.Substring(i);


                            }
                            else if (ycopy[i] == 's' || ycopy[i] == 'c' || ycopy[i] == 'l' || ycopy[i] == 't')
                            {
                                while (ycopy[i] != 'x' || Int32.TryParse(Convert.ToString(ycopy[i]), out numb))
                                {
                                    j = i;
                                    i++;
                                }
                                maths = ycopy.Substring(j, i);
                                ycopy = ycopy.Substring(i);


                            }
                            else if (ycopy[i] == ' ')
                            {
                                i++;
                                ycopy = ycopy.Substring(i);

                            }
                            else if (ycopy[i] == 'x')
                            {
                                numb = x;
                                i++;
                                ycopy = ycopy.Substring(i);
                                i = 0;
                            }
                            else
                            {
                                Console.WriteLine("Функция введена неправельно");
                                ycopy = "";
                                y = "";
                            }                              
                            if (numb != 0 && result == 0)
                            {
                                result = numb;
                                numb = 0;
                            }
                            if (numb !=0 && znak != ' ')
                            {
                                if (znak == '+')
                                    result = result + numb;

                                else  if (znak == '-')
                                    result = result - numb;

                                else if (znak == '*')
                                    result = result * numb;

                                else if (znak == '/')
                                    result = result / numb;

                                znak = ' ';
                            }                           
                            
                        }
                        count = 0;
                        if (y != "")
                        {
                            yleft = y.Substring(0, pastOpen - 1) + Convert.ToString(result);
                            yright = y.Substring(firstClose + 1);
                            y = yleft + yright;
                            y = Decoupling(y);
                            if (Int32.TryParse(Convert.ToString(y.Substring(1,y.Length-2)), out result))
                                y = "";
                        }
                        
                        
                    }

                }
                
            }
                return(result);
        }       
        static void Building(char[,] graphic,string y)
        {
            int newY;
            for (int i = -50; i < 51; i++)
            {
                newY = Сounting(y, i);
                if (newY < 51 && newY > -51) 
                graphic[i + 50, newY+50] = '*';
            }
            for (int i = 0; i < 101; i++)
            {
                for (int j = 100; j >= 0; j--)
                {
                    Console.Write(graphic[i, j]);
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            char[,] graphick = new char[101,101];
            for (int i = 0; i < 101; i++)
            {
                for (int j = 0; j < 101; j++)
                {
                    if(j==51)
                        graphick[i, j] = '|';
                    else if(i==51)
                        graphick[i, j] = '-';
                    else
                    graphick[i, j] = ' ';
                    
                }
            }
            bool cycle = false;
            string function="",inputX="";
            int x;

            while (!cycle)
            {
                Console.WriteLine("Введите функцию/nТипа f(x)=(x+3)^2 или y=sin(x)");

                function = Check(Console.ReadLine().ToLower().Trim());

                for (int i = 0; i < function.Length; i++)
                {
                    if (function[i] == 'x')
                        cycle = true;
                }
                if(!cycle)
                Console.WriteLine("Это точка а не фукция\nAунция должна иметь х");
            }
            
            
            while (true)
            {
                Console.WriteLine("Введите x");
                inputX = Console.ReadLine().Trim();
                if(Int32.TryParse(inputX,out x))
                {
                    x = Convert.ToInt32(inputX);
                    break;
                }
                Console.WriteLine("Введите число, а не букву");
            }            
            function = Decoupling(function);
            Console.WriteLine(Сounting(function,x));
            Building(graphick, function);
            
        }
    }
}
