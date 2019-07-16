using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// 留言管理类
/// </summary>
public class RewriteMgr
{
	public RewriteMgr()
	{
        
	}

    #region　函数UpdateRewrite | 更新Rewrite信息
    /// <summary>
    /// 更新Rewrite类信息
    /// </summary>
    /// <param name="rewrite">留言管理类参数</param>
    /// <returns>更新成功返回true 否则返回false</returns>
    public bool UpdateRewrite(Rewrite rewrite)
    {
        string strSQL = "";
        SqlDataAdapter sa = null;
        SqlCommandBuilder builder = null;
        DataRow row = null;
        DataTable dt = null;
        bool returnValue = false;
        if (rewrite != null)
        {
            if (rewrite.RewriteID == 0)
            {
                strSQL = "SELECT Top 0 * FROM Sys_Rewrite";
            }
            else
            {
                strSQL = "SELECT Top 1 * FROM Sys_Rewrite WHERE RewriteID = " + rewrite.RewriteID.ToString();
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

            row["Title"] = rewrite.Title;
            row["Content"] = rewrite.Content;
            row["InputDate"] = rewrite.InputDate;
            row["PersonID"] = rewrite.Person.PersonID;
            row["ClientID"] = rewrite.Client.ClientID;           
            row["ReContent"] = rewrite.ReContent;

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(row);
            }

            SqlConnection conn = CMMgr.GetConnection();
            using (sa = new SqlDataAdapter("SELECT Top 0 * FROM Sys_Rewrite", conn))
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

    #region　函数GetRewrite | 获取Rewrite信息
    /// <summary>
    /// 获取一条Rewrite类信息
    /// </summary>
    /// <param name="RewriteID">登录编号</param>
    /// <returns>一条Rewrite类记录</returns>
    public Rewrite GetRewrite(string RewriteID)
    {
        Rewrite rewrite = new Rewrite();
        ClientMgr g2Mgr = new ClientMgr();
        PersonMgr pMgr = new PersonMgr();

        string strSQL = "SELECT Top 1 * FROM Sys_Rewrite WHERE RewriteID = " + RewriteID;
        DataTable dt = CMMgr.GetDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            rewrite.RewriteID = int.Parse(row["RewriteID"].ToString());
            rewrite.Title = row["Title"].ToString();
            rewrite.Content = row["Content"].ToString();
            rewrite.InputDate = DateTime.Parse(row["InputDate"].ToString());
            rewrite.Person = pMgr.GetPerson(row["PersonID"].ToString());
            rewrite.Client = g2Mgr.GetClient(row["ClientID"].ToString());            
            rewrite.ReContent = row["ReContent"].ToString();

            return rewrite;
        }
        else
        {
            return rewrite;
        }
    }
    #endregion

    #region　函数DelRewrite | 删除Rewrite信息
    /// <summary>
    /// 删除Rewrite类信息
    /// </summary>
    /// <param name="RewriteID">登录编号参数</param>
    public void DelRewrite(string RewriteID)
    {
        string strSQL = "DELETE FROM Sys_Rewrite WHERE RewriteID = " + RewriteID;
        CMMgr.ExecuteNonQuery(strSQL);
    }

    public void DelRewrite(int RewriteID)
    {
        this.DelRewrite(RewriteID.ToString());
    }
    #endregion

    #region 函数 GetRewriteList | 获取留言信息列表
    /// <summary>
    /// 函数 GetRewriteList | 获取留言信息列表
    /// </summary>
    /// <returns>留言列表</returns>
    public DataTable GetRewriteList(string PersonID,string ClientID)
    {
        string strSQL = @"SELECT 
                            *
                            ,(SELECT PersonName FROM Sys_Person WHERE PersonID = Sys_Rewrite.PersonID) AS PersonName 
                            ,(SELECT ClientName FROM Sys_Client WHERE ClientID = Sys_Rewrite.ClientID) AS ClientName 
                          FROM 
                            Sys_Rewrite 
                          WHERE 1=1 ";
        if (PersonID != "")
        {
            strSQL += "AND PersonID = '" + PersonID + @"' ";
        }

        if (ClientID != "")
        {
            strSQL += "AND ClientID = '" + ClientID + @"' ";
        }

        strSQL += "Order By RewriteID Desc";
        return CMMgr.GetDataTable(strSQL);
    }

    #endregion

}
