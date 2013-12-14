using System;

namespace PersistenceLayer
{
	/// <summary>
	///		This class contains information about association map.
	/// </summary>
	sealed class Association
	{
		private CardinalityTypes cardinality = CardinalityTypes.None;
		private bool saveAutomatic=false;
		private bool deleteAutomatic=false;
		private bool retrieveAutomaitic=false;

		private string toClass =".";
		private string fromClass = ".";

		private string from=".";
		private string to=".";
		private string associationTo=".";

		//Constructor 1
		internal Association()
		{
		}

		/*所有属性*/
		//关联类型(1-1,1-N)
		public CardinalityTypes Cardinality
		{
			get{return this.cardinality;}
			set{this.cardinality=value;}
		}
		
		//关联类是否自动保存
		public bool SaveAutomatic
		{
			get{return this.saveAutomatic;}
			set{this.saveAutomatic=value;}
		}

		//关联类是否自动删除
		public bool DeleteAutomatic
		{
			get{return this.deleteAutomatic;}
			set{this.deleteAutomatic=value;}
		}

		//关联类是否自动获取
		public bool RetrieveAutomatic
		{
			get{return this.retrieveAutomaitic;}
			set{this.retrieveAutomaitic=value;}
		}

		//关联的实体类
		public string ToClass
		{
			get{return this.toClass;}
			set{this.toClass=value;}
		}
		
		//关联的实体类
		public string FromClass
		{
			get{return this.fromClass;}
			set{this.fromClass=value;}
		}

		public string FromAttribute
		{
			get{return this.from;}
			set{this.from=value;}
		}

		public string ToAttribute
		{
			get{return this.to;}
			set{this.to=value;}
		}

		public string Target
		{
			get{return this.associationTo;}
			set{this.associationTo=value;}
		}
	}
	
}
