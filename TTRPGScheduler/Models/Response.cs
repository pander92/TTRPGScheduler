using System;
namespace TTRPGScheduler.Models
{
	public class Response
	{
        public int statusCode { get; set; }
		public string statusDescription { get; set; }
		public List<ProposedSession> sdata { get; set; }
        public List<Player> pdata { get; set; }
        public PlayerAttendance adata { get; set; }
        public Response()
		{

		}
        public Response(int rstatusCode, string rstatusdesc, List<ProposedSession> rlist)
        {
			statusCode = rstatusCode;
			statusDescription = rstatusdesc;
			sdata = rlist;
        }
        public Response(int rstatusCode, string rstatusdesc, List<Player> rlist)
        {
            statusCode = rstatusCode;
            statusDescription = rstatusdesc;
            pdata = rlist;
        }
        public Response(int rstatusCode, string rstatusdesc, PlayerAttendance rlist)
        {
            statusCode = rstatusCode;
            statusDescription = rstatusdesc;
            adata = rlist;
        }
        public Response(int rstatusCode, string rstatusdesc)
        {
            statusCode = rstatusCode;
            statusDescription = rstatusdesc;
        }
    }
}


