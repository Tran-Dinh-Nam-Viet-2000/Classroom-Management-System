using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Constants
{
    public class Messages
    {
        //Is null
        public const string RoomTypeIsNull = "Room type does not exist, please try again";
        public const string ClassIsNull = "Class does not exist, please try again";
        public const string RoomIsNull = "Room does not exist, please try again";
        public const string AccountIsNull = "Account does not exist, please try again";
        public const string SubjectIsNull = "Subject does not exist, please try again";
        public const string RecordIsNull = "Record does not exist, please try again";
        public const string EmailIsNull = "Email does not exist, please try again";

        //Success
        public const string DataIsNotNull = "Success";

        //Fail
        public const string Fail = "Please enter correctly";

        //Successfully(Add,Update,Delete,Restore)
        public const string SuccessfullyAddedNew = "Successfully added new";
        public const string SuccessfullyRestored = "Successfully restored";
        public const string SuccessfullyDeleted = "Successfully deleted";
        public const string SuccessfullyUpdated = "Successfully updated";
        public const string SuccessfullyLogined = "Successfully logined in";
        public const string SuccessfullyApproved = "Successfully approved";
        public const string SuccessfullyRejected = "Successfully rejected";
        public const string ChangePasswordSuccessfully = "Change password successfully";

        //Already exists
        public const string RecordAlreadyExists = "Record already exists, please try again";
        public const string ClassAlreadyExists = "Class already exists, please try again";
        public const string RoomAlreadyExists = "Room already exists, please try again";
        public const string RoomTypeAlreadyExists = "RoomType already exists, please try again";
        public const string AccountAlreadyExists = "Account already exists, please try again";
        public const string SubjectAlreadyExists = "Subject already exists, please try again";
    }
}

