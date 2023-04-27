using System;
namespace TTRPGScheduler.Models
{
	public class Response
	{
        public int statusCode { get; set; }
		public string statusDescription { get; set; }
		public List<ProposedSession> responseData { get; set; }
        public Response()
		{

		}
        public Response(int rstatusCode, string statusdesc, List<ProposedSession> list)
        {
			statusCode = rstatusCode;
			statusDescription = statusdesc;
			responseData = list;
        }
    }
}


