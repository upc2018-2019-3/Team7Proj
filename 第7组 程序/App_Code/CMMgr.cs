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
/// �������һЩ�������ݿ�ķ���
/// </summary>
public class CMMgr
{
    
	#region ����CMMgr���˽�б���
	private SqlCommand m_Cmd = null;
	private SqlConnection m_Conn = null;
	private SqlDataAdapter m_daTer = null;
	private SqlTransaction m_Trans = null;
	#endregion

	#region CMMgr��ľ�̬����
	/// <summary>
	/// �������ݿ������ַ���
	/// </summary>
	static public string GetConnectionString()
	{
        string LinkString = "";        
        LinkString = WebConfigReader.GetDatabaseString();
        return LinkString;
	}

    /// <summary>
    /// �������ݿ������ַ���
    /// </summary>
    /// <param name="strLinkString">���ݿ������ַ���</param>
    /// <returns>���ݿ������ַ���</returns>
    static public string GetConnectionString(string LinkString)
    {
        return LinkString;
    }

	/// <summary>
	/// ����ĳһ���ݿ��еı��
	/// </summary>
	/// <param name="sysid">�������</param>
	/// <returns></returns>
	static public int GetNextIDValue(string sysid)
	{
		int intPrimary = (int)CMMgr.GetDataTable("SELECT " + sysid + " FROM Sys_FieldID").Rows[0][sysid];
		ExecuteNonQuery("UPDATE Sys_FieldID SET " + sysid + "=" + sysid + "+1");
		return intPrimary;
	}

	/// <summary>
	/// ����һ���򿪵��������ݿ����SqlConnection
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
	/// ����һ���������ݱ�
	/// </summary>
	/// <param name="strSQL">SQL��ѯ���</param>
	/// <returns>DataTable��</returns>
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
    /// ����һ���������ݱ�
    /// </summary>
    /// <param name="strSQL">SQL��ѯ���</param>
    /// <param name="Parms">��������</parms>
    /// <returns>DataTable��</returns>
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
    /// ����һ���������ݱ�
    /// </summary>
    /// <param name="strSQL">SQL��ѯ���</param>
    /// <param name="arList">��̬����</param>
    /// <returns>DataTable��</returns>
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
	/// ����һ���������ݱ�
	/// </summary>
	/// <param name="cmd">SqlCommand����</param>
	/// <returns>DataTable��</returns>
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
	/// ִ���޷��ؽ����SQL���
	/// </summary>
	/// <param name="strSQL">Ҫִ�е�SQL���</param>
	/// <returns>������Ӱ�������</returns>
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
	/// ִ���޷��ؽ����SQL���
	/// </summary>
	/// <param name="cmd">Ҫִ�е�SqlCommand����</param>
	/// <returns>������Ӱ�������</returns>
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
    /// ִ�з��ص�һ�е�һ�е�SQL���
    /// </summary>
    /// <param name="strSQL">Ҫִ�е�SQL���</param>
    /// <returns>������Ӱ�������</returns>
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
    /// ִ�з��ص�һ�е�һ�е�SQL���
    /// </summary>
    /// <param name="strSQL">Ҫִ�е�SQL���</param>
    /// <param name="arList">��̬����</param>
    /// <returns>������Ӱ�������</returns>
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
    /// ִ�з��ص�һ�е�һ�е�SQL���
    /// </summary>
    /// <param name="cmd">Ҫִ�е�SqlCommand����</param>
    /// <returns>������Ӱ�������</returns>
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

	#region ����CMMgr���ʹ�ú���
	/// <summary>
	/// ��ʼ���������ݿ����
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
	/// �ر���
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
	/// ��ʼһ��������
	/// </summary>
	public void StartTransaction()
	{
		this.m_Trans = this.m_Conn.BeginTransaction();
		this.m_Cmd.Connection = this.m_Conn;
		this.m_Cmd.Transaction = this.m_Trans;
	}

	/// <summary>
	/// �ύ���һ������
	/// </summary>
	public void Commit()
	{
		if (m_Trans != null)
		{
			this.m_Trans.Commit();
		}
	}

	/// <summary>
	/// �ع�һ������
	/// </summary>
	public void Rollback()
	{
		if (m_Trans != null)
		{
			this.m_Trans.Rollback();
		}
	}

	/// <summary>
	/// ΪCommand�������ò���
	/// </summary>
	/// <param name="DBName">����</param>
	/// <param name="DBValue">ֵ</param>
	/// <param name="dbtype">����</param>
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
	/// ���Command������в���
	/// </summary>
	public void ClearParameter()
	{
		this.m_Cmd.Parameters.Clear();
	}

	/// <summary>
	/// ִ��һ���޷���ֵ�����
	/// </summary>
	/// <returns>Ӱ�������</returns>
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
	/// ����һ���������ݱ�
	/// </summary>
	/// <returns>�������ݱ�</returns>
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

	#region CMMgr������
	/// <summary>
	/// Ҫִ�е�SQL���
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
	/// Ҫִ�е�SQL�������
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
	/// ���û�ȡ��SqlConnection����
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
	/// ���û�ȡ��SqlCommand����
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
	/// ���û�ȡ��SqlDataAdapter����
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