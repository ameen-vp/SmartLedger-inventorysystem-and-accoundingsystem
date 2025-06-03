

namespace Applications.ApiResponse
{
   public class Apiresponse <T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public bool? Success { get; set; }
        public int ? Statuscode { get; set; }
        //public Apiresponse() { }

        //public Apiresponse(T data, string message,bool success,int statuscode)
        //{
        //    Data = data;
        //    Message = message;
        //    Success = success;
        //    Statuscode = statuscode;
        //}


    
    }
}
