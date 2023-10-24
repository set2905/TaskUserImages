using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Errors
{
    public static class DomainErrors
    {
        public static class User
        {
            public static Result NotFound => Result.NotFound("User not found");

        }
        public static class Friendship
        {
            public static Result CheckFriendlist => Result.Error("Couldnt check user existense in friendlist");
            public static Result CheckPendingRequest => Result.Error("Couldnt check for pending friendship request");
            public static Result RequestSentToSelf => Result.Conflict("Cant send friend request to yourself");

        }
        public static class Image
        {
            public static Result NotFound => Result.NotFound("Image not found");
            public static Result CouldNotSaveFile => Result.Error("Image file could not be saved");
            public static Result CouldNotSaveFilePath => Result.Error("Image file path could not be saved");
            public static Result WrongFormat => Result.Error("Wrong file format");

        }
    }
}
