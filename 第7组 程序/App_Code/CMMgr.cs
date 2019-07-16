using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// 此类包含一些操作数据库的方法
/// </summary>
public class CMMgr
{
    
	#region 定义CMMgr类的私有变量
	private SqlCommand m_Cmd = null;
	private SqlConnection m_Conn = null;
	private SqlDataAdapter m_daTer = null;
	private SqlTransaction m_Trans = null;
	#endregion

	#region CMMgr类的静态函数
	/// <summary>
	/// 返回数据库连接字符串
	/// </summary>
	static public string GetConnectionString()
	{
        string LinkString = "";        
        LinkString = WebConfigReader.GetDatabaseString();
        return LinkString;
	}

    /// <summary>
    /// 返回数据库连接字符串
    /// </summary>
    /// <param name="strLinkString">数据库连接字符串</param>
    /// <returns>数据库连接字符串</returns>
    static public string GetConnectionString(string LinkString)
    {
        return LinkString;
    }

	/// <summary>
	/// 返回某一数据库中的编号
	/// </summary>
	/// <param name="sysid">主键编号</param>
	/// <returns></returns>
	static public int GetNextIDValue(string sysid)
	{
		int intPrimary = (int)CMMgr.GetDataTable("SELECT " + sysid + " FROM Sys_FieldID").Rows[0][sysid];
		ExecuteNonQuery("UPDATE Sys_FieldID SET " + sysid + "=" + sysid + "+1");
		return intPrimary;
	}

	/// <summary>
	/// 返回一个打开的连接数据库对象SqlConnection
	/// </summary>
	static public SqlConnection GetConnection()
	{
        
        SqlConnection conn = null;        
        try
        {
            if (((HttpApplicationState)HttpContext.Current.Application)["AppConn"] == null)
            {
                conn = new SqlConnection(GetConnectionString());
                conn.Open();
                ((HttpApplicationState)HttpContext.Current.Application)["AppConn"] = conn;
            }
            else
            {
                try
                {
                    conn = (SqlConnection)(((HttpApplicationState)HttpContext.Current.Application)["AppConn"]);
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                }
                catch
                {
                    conn = new SqlConnection(GetConnectionString());
                    conn.Open();
                    ((HttpApplicationState)HttpContext.Current.Application)["AppConn"] = conn;
                }
            }
        }
        catch 
        {
            conn = new SqlConnection(GetConnectionString());
            conn.Open();
            ((HttpApplicationState)HttpContext.Current.Application)["AppConn"] = conn;
        }
		return conn;
	}

	/// <summary>
	/// 返回一个填充的数据表
	/// </summary>
	/// <param name="strSQL">SQL查询语句</param>
	/// <returns>DataTable表</returns>
	static public DataTable GetDataTable(string strSQL)		
	{
		SqlConnection conn = GetConnection();
		DataTable dt = new DataTable();

		using(SqlDataAdapter da = new SqlDataAdapter(strSQL,conn))
		{
            try
            {
                da.FillSchema(dt, SchemaType.Mapped);
                da.Fill(dt);
            }
            catch { }
		}
		return dt;
	}

    /// <summary>
    /// 返回一个填充的数据表
    /// </summary>
    /// <param name="strSQL">SQL查询语句</param>
    /// <param name="Parms">参数数组</parms>
    /// <returns>DataTable表</returns>
    static public DataTable GetDataTable(string strSQL, SqlParameter[] Parms)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(strSQL, GetConnection());
        foreach (SqlParameter Parm in Parms)
        {
            cmd.Parameters.Add(Parm);
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {                
            da.FillSchema(dt, SchemaType.Mapped);
            da.Fill(dt);
        }
        catch { }
        return dt;
    }

    /// <summary>
    /// 返回一个填充的数据表
    /// </summary>
    /// <param name="strSQL">SQL查询语句</param>
    /// <param name="arList">动态数组</param>
    /// <returns>DataTable表</returns>
    static public DataTable GetDataTable(string strSQL, ArrayList arList)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(strSQL, GetConnection());
        foreach (SqlParameter Parm in arList)
        {
            cmd.Parameters.Add(Parm);
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.FillSchema(dt, SchemaType.Mapped);
            da.Fill(dt);
        }
        catch { }
        return dt;
    }

	/// <summary>
	/// 返回一个填充的数据表
	/// </summary>
	/// <param name="cmd">SqlCommand对象</param>
	/// <returns>DataTable表</returns>
	static public DataTable GetDataTable(SqlCommand cmd)
	{
		DataTable dt = new DataTable();
        try
        {
            if (cmd.Connection == null)
            {
                cmd.Connection = GetConnection();
            }
            else if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.FillSchema(dt, SchemaType.Mapped);
            da.Fill(dt);
        }
        catch { }
		return dt;
	}

	/// <summary>
	/// 执行无返回结果的SQL语句
	/// </summary>
	/// <param name="strSQL">要执行的SQL语句</param>
	/// <returns>返回受影响的行数</returns>
	public static int ExecuteNonQuery(string strSQL)
	{
		SqlCommand cmd = null;
		int i = 0;
		cmd = new SqlCommand(strSQL,GetConnection());
        try
        {
            i = cmd.ExecuteNonQuery();
        }
        catch { }
		return i;
	}

	/// <summary>
	/// 执行无返回结果的SQL语句
	/// </summary>
	/// <param name="cmd">要执行的SqlCommand对象</param>
	/// <returns>返回受影响的行数</returns>
	public static int ExecuteNonQuery(SqlCommand cmd)
	{
		int i = 0;
		cmd.Connection = GetConnection();
		if(cmd.Connection.State != ConnectionState.Open)
		{
			cmd.Connection.Open();
		}
        try
        {
            i = cmd.ExecuteNonQuery();
        }
        catch { }
		return i;
	}

    /// <summary>
    /// 执行返回第一行第一列的SQL语句
    /// </summary>
    /// <param name="strSQL">要执行的SQL语句</param>
    /// <returns>返回受影响的行数</returns>
    public static object ExecuteScalar(string strSQL)
    {
        SqlCommand cmd = null;
        object obj = new object();
        cmd = new SqlCommand(strSQL, GetConnection());
        try
        {
            obj = cmd.ExecuteScalar();
        }
        catch { }
        return obj;
    }

    /// <summary>
    /// 执行返回第一行第一列的SQL语句
    /// </summary>
    /// <param name="strSQL">要执行的SQL语句</param>
    /// <param name="arList">动态数组</param>
    /// <returns>返回受影响的行数</returns>
    public static object ExecuteScalar(string strSQL, ArrayList arList)
    {
        SqlCommand cmd = null;
        object obj = new object();
        cmd = new SqlCommand(strSQL, GetConnection());
        foreach (SqlParameter Parm in arList)
        {
            cmd.Parameters.Add(Parm);
        }

        try
        {
            obj = cmd.ExecuteScalar();
        }
        catch { }
        return obj;
    }

    /// <summary>
    /// 执行返回第一行第一列的SQL语句
    /// </summary>
    /// <param name="cmd">要执行的SqlCommand对象</param>
    /// <returns>返回受影响的行数</returns>
    public static object ExecuteScalar(SqlCommand cmd)
    {
        object obj = new object();
        cmd.Connection = GetConnection();
        if (cmd.Connection.State != ConnectionState.Open)
        {
            cmd.Connection.Open();
        }
        try
        {
            obj = cmd.ExecuteScalar();
        }
        catch { }
        return obj;
    }
	#endregion

	#region 定义CMMgr类的使用函数
	/// <summary>
	/// 初始化连接数据库变量
	/// </summary>
    public CMMgr(string CommandText, string LinkString)
    {
        try
        {
            if (LinkString == "")
            {
                m_Conn = new SqlConnection(CMMgr.GetConnectionString());
            }
            else
            {
                m_Conn = new SqlConnection(LinkString);
            }
            m_Conn.Open();
            m_Cmd = m_Conn.CreateCommand();
            m_Cmd.CommandText = CommandText;
            m_daTer = new SqlDataAdapter(m_Cmd);
        }
        catch { }
    }

	public CMMgr(string CommandText) : this(CommandText,"")
	{
		
	}

    public CMMgr()
        : this("")
    {

    }

	/// <summary>
	/// 关闭类
	/// </summary>
	public void Close()
	{
        if (this.m_Conn != null)
        {
            m_Conn.Close();
            m_Conn.Dispose();
        }
        if (this.m_daTer != null)
        {
            m_daTer.Dispose();
        }
        if (this.m_Cmd != null)
        {
            m_Cmd.Dispose();
        }
		if (m_Trans != null)
		{
			m_Trans.Dispose();
		}
	}

	/// <summary>
	/// 开始一个事务处理
	/// </summary>
	public void StartTransaction()
	{
		this.m_Trans = this.m_Conn.BeginTransaction();
		this.m_Cmd.Connection = this.m_Conn;
		this.m_Cmd.Transaction = this.m_Trans;
	}

	/// <summary>
	/// 提交完成一个事务
	/// </summary>
	public void Commit()
	{
		if (m_Trans != null)
		{
			this.m_Trans.Commit();
		}
	}

	/// <summary>
	/// 回滚一项事务
	/// </summary>
	public void Rollback()
	{
		if (m_Trans != null)
		{
			this.m_Trans.Rollback();
		}
	}

	/// <summary>
	/// 为Command变量设置参数
	/// </summary>
	/// <param name="DBName">名称</param>
	/// <param name="DBValue">值</param>
	/// <param name="dbtype">类型</param>
	public void SetParameter(string DBName,object DBValue,SqlDbType dbtype)
    {
        try
        {
            if (dbtype == SqlDbType.UniqueIdentifier)
            {
                this.m_Cmd.Parameters.Add(new SqlParameter(DBName, dbtype));
                this.m_Cmd.Parameters[DBName].Value = new Guid(DBValue.ToString());
            }
            else
            {
                this.m_Cmd.Parameters.Add(new SqlParameter(DBName, dbtype));
                this.m_Cmd.Parameters[DBName].Value = DBValue;
            }
        }
        catch { }
    }

	/// <summary>
	/// 清除Command里的所有参数
	/// </summary>
	public void ClearParameter()
	{
		this.m_Cmd.Parameters.Clear();
	}

	/// <summary>
	/// 执行一个无返回值的语句
	/// </summary>
	/// <returns>影响的行数</returns>
	public int ExecuteNonQuery()
	{
		int returnValue = 0;
        try
        {
            returnValue = this.m_Cmd.ExecuteNonQuery();
        }
        catch { }
		return returnValue;
	}

	/// <summary>
	/// 返回一个填充的数据表
	/// </summary>
	/// <returns>填充的数据表</returns>
	public DataTable GetDataTable()
	{
		DataTable dt = new DataTable();
        try
        {
            this.m_daTer.SelectCommand = this.m_Cmd;
            this.m_daTer.Fill(dt);
        }
        catch { }
		return dt;
	}
	#endregion

	#region CMMgr类属性
	/// <summary>
	/// 要执行的SQL语句
	/// </summary>
	public string CommandText
	{
		set
		{
			this.m_Cmd.CommandText = value;
		}
		get
		{
			return this.m_Cmd.CommandText;
		}
	}

	/// <summary>
	/// 要执行的SQL语句类型
	/// </summary>
	public CommandType CommandType
	{
		set
		{
			this.m_Cmd.CommandType = value;
		}
		get
		{
			return this.m_Cmd.CommandType;
		}
	}

	/// <summary>
	/// 设置或取得SqlConnection对象
	/// </summary>
	public SqlConnection Connection
	{
		set
		{
			if (value != null)
			{
				this.m_Conn = value;
			}
		}
		get
		{
			return this.m_Conn;
		}
	}

	/// <summary>
	/// 设置或取得SqlCommand对象
	/// </summary>
	public SqlCommand Command
	{
		set
		{
			if (value != null)
			{
				this.m_Cmd = value;
			}
		}
		get
		{
			return this.m_Cmd;
		}
	}

	/// <summary>
	/// 设置或取得SqlDataAdapter对象
	/// </summary>
	public SqlDataAdapter DataAdapter
	{
		set
		{
			if (value != null)
			{
				this.m_daTer = value;
			}
		}
		get
		{
			return this.m_daTer;
		}
	}
	#endregion
}