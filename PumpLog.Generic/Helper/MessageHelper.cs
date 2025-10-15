using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumpLog.Generic.Helper
{
    public class MessageHelper
    {
        public const string genericException = "Something went wrong, Please contact system administrator.";
        public const string invalidCredentials = "Username or Password is Incorrect!";
        public const string userNotFound = "User not found.";
        public const string userNotFoundForEmail = "Reset link will be provided on Email.";
        public const string userDeleted = "The User has been Deleted!";
        public const string noDataFound = "No Data Found.";
        public const string userNotRegister = "User is not Registered with the system.";
        public const string resetPasswordlin = "Reset Password Link is sent on email, please check your email for Reset Password Link.";
        public const string passwordSame = "NEW PASSWORD CANNOT BE SAME AS CURRENT PASSWORD";
        public const string wrongPassword = "CURRENT PASSOWRD DOES NOT MATCH.";
        public const string wrongEmail = "CURRENT EMAIL DOES NOT MATCH.";
        public const string emailSame = "NEW EMAIL CANNOT BE SAME AS CURRENT EMAIL";
        public const string invalidOtp = "OTP is not Valid";
        public const string createPasswordlin = "Create Password Link is sent on email, please check your email for Reset Password Link.";
        public const string message = "Internal Error has Occurred. Please Contact Your Administrator";
    }
}
