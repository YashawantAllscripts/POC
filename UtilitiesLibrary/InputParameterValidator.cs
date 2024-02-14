using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesLibrary
{
    public class InputParameterValidator
    {
        //private static instance variable
        private static InputParameterValidator _inputParameterValidator;

        
      /// <summary>
      /// Private Constructor to restrict a creating new object
      /// </summary>
        private InputParameterValidator() {}

        //public method to get singleton instances
        public static InputParameterValidator inputParameterValidator 
        {
            get 
            { 
                //If Instance is null, create a new instance
                if(_inputParameterValidator== null)
                {
                    _inputParameterValidator = new InputParameterValidator();
                }
                return _inputParameterValidator;
            }
        }

        //Check whether Id is greater than 0 and less than 10000   
        public bool IdValidator(int Id)
        {
            if (Id > 0 && Id < 10000) 
            { 
                return true;
            }
            else
                return false;
        }

        public bool ValidateString(string strId)
        {
            if (string.IsNullOrEmpty(strId)) 
            {
                Console.WriteLine("Input String is Empty or Null");
                return false;
            }
            if(int.TryParse(strId,out _))
            {
                return true;
            }
            else 
            {
                Console.WriteLine("Invalid Input string. Please Enter valid Input");
                return false; 
            }
        }
    }
}
