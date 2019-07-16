using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// 教师管理类
/// </summary>
public class ClientMgr
{
	public ClientMgr()
	{
        
	}

    #region　函数UpdateClient | 更新Client信息
    /// <summary>
    /// 更新Client类信息
    /// </summary>
    /// <param name="client">教师管理类参数</param>
    /// <returns>更新成功返回true 否则返回false</returns>
    public bool UpdateClient(Client client)
    {
        string strSQL = "";
        SqlDataAdapter sa = null;
        SqlCommandBuilder builder = null;
        DataRow row = null;
        DataTable dt = null;
        bool returnValue = false;
        if (client != null)
        {
            if (client.ClientID == "")
            {
                strSQL = "SELECT Top 0 * FROM Sys_Client";
            }
            else
            {
                strSQL = "SELECT Top 1 * FROM Sys_Client WHERE ClientID = '" + client.ClientID + "'";
            }

            dt = CMMgr.GetDataTable(strSQL);

            if (dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
            }
            else
            {
                row = dt.NewRow();
            }

            row["ClientID"] = client.ClientID;
            row["ClientName"] = client.ClientName;
            row["Password"] = client.Password;
            row["Sex"] = client.Sex;
            row["ClassInfoID"] = client.ClassInfo.ClassInfoID;
            row["SpecID"] = client.Spec.SpecID;
            row["Tel"] = client.Tel;
            row["Mail"] = client.Mail;
            row["QQ"] = client.QQ;

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(row);
            }

            SqlConnection conn = CMMgr.GetConnection();
            using (sa = new SqlDataAdapter("SELECT Top 0 * FROM Sys_Client", conn))
            {
                try
                {
                    builder = new SqlCommandBuilder(sa);
                    sa.Update(dt);
                    returnValue = true;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        return returnValue;
    }
    #endregion

    #region　函数GetClient | 获取Client信息
    /// <summary>
    /// 获取一条Client类信息
    /// </summary>
    /// <param name="ClientID">登录编号</param>
    /// <returns>一条Client类记录</returns>
    public Client GetClient(string ClientID)
    {
        Client client = new Client();
        string strSQL = "SELECT Top 1 * FROM Sys_Client WHERE ClientID = '" + ClientID + "'";
        ClassInfoMgr cMgr = new ClassInfoMgr();
        SpecMgr sMgr = new SpecMgr();

        DataTable dt = CMMgr.GetDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            client.ClientID = row["ClientID"].ToString();
            client.ClientName = row["ClientName"].ToString();
            client.Password = row["Password"].ToString();
            client.Sex = row["Sex"].ToString();
            client.ClassInfo = cMgr.GetClassInfo(row["ClassInfoID"].ToString());
            client.Spec = sMgr.GetSpec(row["SpecID"].ToString());
            client.Tel = row["Tel"].ToString();
            client.Mail = row["Mail"].ToString();
            client.QQ = row["QQ"].ToString();
            
            return client;
        }
        else
        {
            return client;
        }
    }
    #endregion

    #region 函数ExistsClient | 判断一个教师登录帐号是否存在
    /// <summary>
    /// 函数ExistsClient | 判断一个教师登录帐号是否存在
    /// </summary>
    /// <param name="ClientID">教师帐号</param>
    /// <returns>存在返回true不存在返回false</returns>
    public bool ExistsClient(string ClientID)
    {
        ArrayList arList = new ArrayList();
        SqlParameter Param = null;
        string strSQL = "SELECT Top 1 ClientID FROM Sys_Client WHERE ClientID = @ClientID";

        Param = new SqlParameter("ClientID", SqlDbType.NVarChar);
        Param.Value = ClientID;
        arList.Add(Param);


        if (CMMgr.GetDataTable(strSQL, arList).Rows.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion

    #region　函数DelClient | 删除Client信息
    /// <summary>
    /// 删除Client类信息
    /// </summary>
    /// <param name="ClientID">登录编号参数</param>
    public void DelClient(string ClientID)
    {
        string strSQL = "DELETE FROM Sys_Client WHERE ClientID = '" + ClientID + "'";
        CMMgr.ExecuteNonQuery(strSQL);
    }

    /// <summary>
    /// 删除Client类信息
    /// </summary>
    /// <param name="client">Client类</param>
    public void DelClient(Client client)
    {
        this.DelClient(client.ClientID);
    }
    #endregion

    #region 函数 GetClientList | 获取教师信息列表
    /// <summary>
    /// 函数 GetClientList | 获取教师信息列表
    /// </summary>
    /// <param name="ClientID">登录名称</param>
    /// <param name="ClientName">真实名称</param>
    /// <returns>登录名称列表</returns>
    public DataTable GetClientList(string ClientID, string ClientName)
    {
        string strSQL = "SELECT  *,(SELECT SpecName FROM Sys_Spec WHERE SpecID = Sys_Client.SpecID) AS SpecName,(SELECT ClassInfoName FROM Sys_ClassInfo WHERE ClassInfoID = Sys_Client.ClassInfoID) AS ClassInfoName  FROM Sys_Client WHERE "
                      + "ClientID Like '%' + @ClientID + '%' "
                      + "AND ClientName Like '%' + @ClientName + '%' ";

        SqlParameter[] Parms = new SqlParameter[2];
        Parms[0] = new SqlParameter("ClientID", SqlDbType.NVarChar);
        Parms[0].Value = ClientID;

        Parms[1] = new SqlParameter("ClientName", SqlDbType.NVarChar);
        Parms[1].Value = ClientName;

        return CMMgr.GetDataTable(strSQL, Parms);
    }

    public DataTable GetClientList(string ClientID)
    {
        return GetClientList(ClientID, "");
    }

    public DataTable GetClientList()
    {
        return GetClientList("");
    }
    #endregion
}
