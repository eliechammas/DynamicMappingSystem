using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataModels.Common
{
    public class ReturnStatusModel
    {
        public ReturnStatus ReturnStatus { get; set; }
        public List<ReturnStatus> ReturnStatuses { get; set; }  

        public ReturnStatusModel()
        {
            ReturnStatus = new ReturnStatus();
            ReturnStatuses = new List<ReturnStatus>();
        }

        #region Generic Errors
        public void OK()
        {
            this.ReturnStatus.ReturnCode = 200;
            this.ReturnStatus.ReturnMessage = "Success";
        }
        public void Access_Denied()
        {
            this.ReturnStatus.ReturnCode = 403;
            this.ReturnStatus.ReturnMessage = "Access_Denied";
        }
        public void Unauthorized()
        {
            this.ReturnStatus.ReturnCode = 401;
            this.ReturnStatus.ReturnMessage = "Unauthorized";
        }
        public void Invalid_Security_Code()
        {
            this.ReturnStatus.ReturnCode = 401;
            this.ReturnStatus.ReturnMessage = "Invalid_Security_Code";
        }
        public void Object_NotFound()
        {
            this.ReturnStatus.ReturnCode = 404;
            this.ReturnStatus.ReturnMessage = "The requested object was not found";
        }
        public void ObjectId_NotExist()
        {
            this.ReturnStatus.ReturnCode = 404;
            this.ReturnStatus.ReturnMessage = "The requested object return nullable Id";
        }
        public void Internal_Server_Error()
        {
            this.ReturnStatus.ReturnCode = 500;
            this.ReturnStatus.ReturnMessage = "Internal_Server_Error";
        }
        public void Invalid_Input()
        {
            this.ReturnStatus.ReturnCode = 422;
            this.ReturnStatus.ReturnMessage = "Invalid Input Data";            
        }
        public void Missing_Required_Field()
        {
            this.ReturnStatus.ReturnCode = 500;
            this.ReturnStatus.ReturnMessage = "Missing_Required_Field";
        }
        public void EndDateUlteriorStartDate()
        {
            this.ReturnStatus.ReturnCode = 500;
            this.ReturnStatus.ReturnMessage = "EndDate_Ulterior_StartDate";
        }
        #endregion

        #region Business Logic Errors
        public void Invalid_Input_Id()
        {
            ReturnStatus returnStatus = new ReturnStatus();
            returnStatus.ReturnCode = 422;
            returnStatus.ReturnMessage = "Invalid Input Data - Id is less or equal than 0!";
            ReturnStatuses.Add(returnStatus);
        }
        public void Invalid_Input_TargetType()
        {
            ReturnStatus returnStatus = new ReturnStatus();
            returnStatus.ReturnCode = 422;
            returnStatus.ReturnMessage = "Invalid Input Data - Target Type is empty!";
            ReturnStatuses.Add(returnStatus);
        }
        public void Invalid_Input_TargetType_NotFound()
        {
            ReturnStatus returnStatus = new ReturnStatus();
            returnStatus.ReturnCode = 422;
            returnStatus.ReturnMessage = "Invalid Input Data - Target Type is not found!";
            ReturnStatuses.Add(returnStatus);
        }
        public void Invalid_Input_SourceType()
        {
            ReturnStatus returnStatus = new ReturnStatus();
            returnStatus.ReturnCode = 422;
            returnStatus.ReturnMessage = "Invalid Input Data - Source Type is empty!";
            ReturnStatuses.Add(returnStatus);
        }
        public void Invalid_Input_SourceType_NotFound()
        {
            ReturnStatus returnStatus = new ReturnStatus();
            returnStatus.ReturnCode = 422;
            returnStatus.ReturnMessage = "Invalid Input Data - Source Type is not found!";
            ReturnStatuses.Add(returnStatus);
        }
        public void Invalid_Input_SourceModel()
        {
            ReturnStatus returnStatus = new ReturnStatus();
            returnStatus.ReturnCode = 422;
            returnStatus.ReturnMessage = "Invalid Input Data - Source Model is null!";
            ReturnStatuses.Add(returnStatus);
        }
        public void Invalid_Input_SourceModel_IncompatibleFormat()
        {
            ReturnStatus returnStatus = new ReturnStatus();
            returnStatus.ReturnCode = 422;
            returnStatus.ReturnMessage = "Invalid Input Data - Source Model has incompatible format!";
            ReturnStatuses.Add(returnStatus);
        }
        public void Invalid_Output_TargetModel()
        {
            ReturnStatus returnStatus = new ReturnStatus();
            returnStatus.ReturnCode = 406;
            returnStatus.ReturnMessage = "Invalid Output Data - Target Model is null!";
            ReturnStatuses.Add(returnStatus);
        }
        public void Invalid_Output_TargetModel_IncompatibleFormat()
        {
            ReturnStatus returnStatus = new ReturnStatus();
            returnStatus.ReturnCode = 406;
            returnStatus.ReturnMessage = "Invalid Output Data - Target Model has incompatible format!";
            ReturnStatuses.Add(returnStatus);
        }

        #endregion
    }

    public class ReturnStatus
    {
        public int ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        
        public ReturnStatus()
        {
            this.ReturnCode = 200;
        }
    }
}
