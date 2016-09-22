using System;
using System.Collections.Generic;

namespace RestDemo
{
	public class TestJsonClass
	{
		public class User
		{
			public int id { get; set; }
			public string first_name { get; set; }
			public string last_name { get; set; }
			public long phone { get; set; }
			public int role_id { get; set; }
			public string created { get; set; }
			public string api_token { get; set; }
			public string api_token_expiration { get; set; }
		}

		public class Return
		{
			public string Status { get; set; }
			public string Message { get; set; }
		}

		public class SessionResponse
		{
			public IList<User> User { get; set; }
			public Return Return { get; set; }
		}

		public class Example
		{
			public SessionResponse SessionResponse { get; set; }
		}
	}
}
