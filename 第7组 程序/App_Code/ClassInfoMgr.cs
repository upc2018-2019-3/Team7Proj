using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// 班级管理类
/// </summary>
public class ClassInfoMgr
{
	public ClassInfoMgr()
	{
        
	}

    #region　函数UpdateClassInfo | 更新ClassInfo信息
    /// <summary>
    /// 更新ClassInfo类信息
    /// </summary>
    /// <param name="classInfo">班级管理类参数</param>
    /// <returns>更新成功返回true 否则返回false</returns>
    public bool UpdateClassInfo(ClassInfo classInfo)
    {
        string strSQL = "";
        SqlDataAdapter sa = null;
        SqlCommandBuilder builder = null;
        DataRow row = null;
        DataTable dt = null;
        bool returnValue = false;
        if (classInfo != null)
        {
            if (classInfo.ClassInfoID == 0)
            {
                strSQL = "SELECT Top 0 * FROM Sys_ClassInfo";
            }
            else
            {
                strSQL = "SELECT Top 1 * FROM Sys_ClassInfo WHERE ClassInfoID = " + classInfo.ClassInfoID.ToString();
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

            row["ClassInfoName"] = classInfo.ClassInfoName;
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(row);
            }

            SqlConnection conn = CMMgr.GetConnection();
            using (sa = new SqlDataAdapter("SELECT Top 0 * FROM Sys_ClassInfo", conn))
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

    #region　函数GetClassInfo | 获取ClassInfo信息
    /// <summary>
    /// 获取一条ClassInfo类信息
    /// </summary>
    /// <param name="ClassInfoID">登录编号</param>
    /// <returns>一条ClassInfo类记录</returns>
    public ClassInfo GetClassInfo(string ClassInfoID)
    {
        ClassInfo classInfo = new ClassInfo();

        string strSQL = "SELECT Top 1 * FROM Sys_ClassInfo WHERE ClassInfoID = " + ClassInfoID;
        DataTable dt = CMMgr.GetDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            classInfo.ClassInfoID = int.Parse(row["ClassInfoID"].ToString());
            classInfo.ClassInfoName = row["ClassInfoName"].ToString();
            return classInfo;
        }
        else
        {
            return classInfo;
        }
    }
    #endregion

    #region　函数DelClassInfo | 删除ClassInfo信息
    /// <summary>
    /// 删除ClassInfo类信息
    /// </summary>
    /// <param name="ClassInfoID">登录编号参数</param>
    public void DelClassInfo(string ClassInfoID)
    {
        string strSQL = "DELETE FROM Sys_ClassInfo WHERE ClassInfoID = " + ClassInfoID;
        CMMgr.ExecuteNonQuery(strSQL);
    }

    public void DelClassInfo(int ClassInfoID)
    {
        this.DelClassInfo(ClassInfoID.ToString());
    }
    #endregion

    #region 函数 GetClassInfoList | 获取班级信息列表
    /// <summary>
    /// 函数 GetClassInfoList | 获取班级信息列表
    /// </summary>
    /// <param name="ClassInfoID">登录名称</param>
    /// <param name="ClassInfoName">真实名称</param>
    /// <returns>班级列表</returns>
    public DataTable GetClassInfoList()
    {
        string strSQL = "SELECT * FROM Sys_ClassInfo Order By ClassInfoID Desc";

        return CMMgr.GetDataTable(strSQL);
    }

    #endregion

}
