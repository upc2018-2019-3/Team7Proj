using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// 管理员管理类
/// </summary>
public class AdminMgr
{
	public AdminMgr()
	{
        
	}

    #region　函数UpdateAdmin | 更新Admin信息
    /// <summary>
    /// 更新Admin类信息
    /// </summary>
    /// <param name="admin">管理员管理类参数</param>
    /// <returns>更新成功返回true 否则返回false</returns>
    public bool UpdateAdmin(Admin admin)
    {
        string strSQL = "";
        SqlDataAdapter sa = null;
        SqlCommandBuilder builder = null;
        DataRow row = null;
        DataTable dt = null;
        bool returnValue = false;
        if (admin != null)
        {
            if (admin.AdminID == "")
            {
                strSQL = "SELECT Top 0 * FROM Sys_Admin";
            }
            else
            {
                strSQL = "SELECT Top 1 * FROM Sys_Admin WHERE AdminID = '" + admin.AdminID + "'";
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

            row["AdminID"] = admin.AdminID;
            row["AdminName"] = admin.AdminName;
            row["Tel"] = admin.Tel;
            row["Mail"] = admin.Mail;
            row["QQ"] = admin.QQ;
            row["Password"] = admin.Password;

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(row);
            }

            SqlConnection conn = CMMgr.GetConnection();
            using (sa = new SqlDataAdapter("SELECT Top 0 * FROM Sys_Admin", conn))
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

    #region　函数GetAdmin | 获取Admin信息
    /// <summary>
    /// 获取一条Admin类信息
    /// </summary>
    /// <param name="AdminID">登录编号</param>
    /// <returns>一条Admin类记录</returns>
    public Admin GetAdmin(string AdminID)
    {
        Admin admin = new Admin();
        string strSQL = "SELECT Top 1 * FROM Sys_Admin WHERE AdminID = '" + AdminID + "'";
        DataTable dt = CMMgr.GetDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            admin.AdminID = row["AdminID"].ToString();
            admin.AdminName = row["AdminName"].ToString();
            admin.Tel = row["Tel"].ToString();
            admin.Mail = row["Mail"].ToString();
            admin.QQ = row["QQ"].ToString();
            admin.Password = row["Password"].ToString();
            return admin;
        }
        else
        {
            return admin;
        }
    }
    #endregion

    #region 函数ExistsAdmin | 判断一个管理员登录帐号是否存在
    /// <summary>
    /// 函数ExistsAdmin | 判断一个管理员登录帐号是否存在
    /// </summary>
    /// <param name="AdminID">管理员帐号</param>
    /// <returns>存在返回true不存在返回false</returns>
    public bool ExistsAdmin(string AdminID)
    {
        ArrayList arList = new ArrayList();
        SqlParameter Param = null;
        string strSQL = "SELECT Top 1 AdminID FROM Sys_Admin WHERE AdminID = @AdminID";

        Param = new SqlParameter("AdminID", SqlDbType.NVarChar);
        Param.Value = AdminID;
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

    #region　函数DelAdmin | 删除Admin信息
    /// <summary>
    /// 删除Admin类信息
    /// </summary>
    /// <param name="AdminID">登录编号参数</param>
    public void DelAdmin(string AdminID)
    {
        string strSQL = "DELETE FROM Sys_Admin WHERE AdminID = '" + AdminID + "'";
        CMMgr.ExecuteNonQuery(strSQL);
    }

    /// <summary>
    /// 删除Admin类信息
    /// </summary>
    /// <param name="admin">Admin类</param>
    public void DelAdmin(Admin admin)
    {
        this.DelAdmin(admin.AdminID);
    }
    #endregion

    #region 函数 GetAdminList | 获取管理员信息列表
    /// <summary>
    /// 函数 GetAdminList | 获取管理员信息列表
    /// </summary>
    /// <param name="AdminID">登录名称</param>
    /// <param name="AdminName">真实名称</param>
    /// <returns>登录名称列表</returns>
    public DataTable GetAdminList(string AdminID, string AdminName)
    {
        string strSQL = "SELECT * FROM Sys_Admin WHERE "
                      + "AdminID Like '%' + @AdminID + '%' "
                      + "AND AdminName Like '%' + @AdminName + '%' ";

        SqlParameter[] Parms = new SqlParameter[2];
        Parms[0] = new SqlParameter("AdminID", SqlDbType.NVarChar);
        Parms[0].Value = AdminID;

        Parms[1] = new SqlParameter("AdminName", SqlDbType.NVarChar);
        Parms[1].Value = AdminName;

        return CMMgr.GetDataTable(strSQL, Parms);
    }

    public DataTable GetAdminList(string AdminID)
    {
        return GetAdminList(AdminID, "");
    }

    public DataTable GetAdminList()
    {
        return GetAdminList("");
    }
    #endregion
}
