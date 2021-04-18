using System;
using System.ServiceModel;
using WCFFaults.Exceptions;

namespace WCFFaults
{
    public class SimpleCalculator : ISimpleCalculator
    {
        public int Divide_1(int Left, int Right)
        {
            return Left / Right;
        }

        public int Divide_2(int Left, int Right)
        {
            if (Right == 0)
                throw new DivideByZeroException();

            return Left / Right;
        }

        public int Divide_3(int Left, int Right)
        {
            if (Right == 0)
                throw new FaultException("Divide by zero", new FaultCode("ZeroDivide"));

            return Left / Right;
        }

        public int Divide_4(int Left, int Right)
        {
            if (Right == 0)
                throw new FaultException<DivideByZeroFault>(new DivideByZeroFault { Message = "Cannot be divided by zero" });

            return Left / Right;
        }
    }
}
