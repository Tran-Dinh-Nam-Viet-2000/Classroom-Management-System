using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Constants
{
    public class Messages
    {
        //Is null
        public const string RoomTypeIsNull = "Room type does not exist";
        public const string ClassIsNull = "Class does not exist";
        public const string RoomIsNull = "Room does not exist";
        public const string AccountIsNull = "Account does not exist";
        public const string SubjectIsNull = "Subject does not exist";

        //Is not null
        public const string DataIsNotNull = "Success";

        //Successfully(Add,Update,Delete,Restore)
        public const string SuccessfullyAddedNew = "Successfully added new";
        public const string SuccessfullyRestored = "Successfully restored";
        public const string SuccessfullyDeleted = "Successfully deleted";
        public const string SuccessfullyUpdated = "Successfully updated";
        public const string SuccessfullyLogined = "Successfully logined in";

        //Already exists
        public const string ClassAlreadyExists = "Class already exists";
        public const string RoomAlreadyExists = "Room already exists";
        public const string RoomTypeAlreadyExists = "RoomType already exists";
        public const string AccountAlreadyExists = "Account already exists";
        public const string SubjectAlreadyExists = "Subject already exists";
    }
}

