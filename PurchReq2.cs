public void setData()
{
    SysOperationProgress simpleProgress;
    str sql;
    Connection Conn;
    Statement Stmt;
    SqlStatementExecutePermission permission;
    ResultSet R;
    PRList _PRList;
    ResultSet resultSet;
    int i = 2;

    SqlStatementExecutePermission perm;
    TMP_PRList TmpList;

    Purchreqtable _Purchreqtable;
    WORKFLOWTRACKINGSTATUSTABLE _WORKFLOWTRACKINGSTATUSTABLE;
    DIRPARTYTABLE _DIRPARTYTABLE;
    WORKFLOWTRACKINGTABLE _WORKFLOWTRACKINGTABLE;
    WORKFLOWTABLE _WORKFLOWTABLE;
    WORKFLOWVERSIONTABLE _WORKFLOWVERSIONTABLE;
    HcmWorker _HcmWorker;
    PurchRFQCaseTable purchRFQCaseTable;
    PurchRFQLine _PurchRFQLine;
    purchRFQTable _purchRFQTable;
    ;
# avifiles

    startLengthyOperation();
    delete_from TmpList;

    /*
  while select * from
    //max(createdDAtetime) from //_Purchreqtable.PurchReqId, purchReqName, reason,
/* PURCHID(),CURRENCY(),_Purchreqtable.Discount(),
 _Purchreqtable.SumAmount(),_Purchreqtable.RFQCaseId(),_Purchreqtable.REQUISITIONSTATUS,
 _Purchreqtable.createdDatetime,_DIRPARTYTABLE.name,
 _WORKFLOWTRACKINGSTATUSTABLE.TRACKINGSTATUS,
 _WORKFLOWTRACKINGTABLE.user_ //, max(_WORKFLOWTRACKINGTABLE.createdDAtetime),
           */
    _WORKFLOWVERSIONTABLE JOIN *from
               _WORKFLOWTRACKINGSTATUSTABLE  where _WORKFLOWTRACKINGSTATUSTABLE.WORKFLOWVERSIONTABLE == _WORKFLOWVERSIONTABLE.recId
                         JOIN firstonly user from
                         _WORKFLOWTRACKINGTABLE
                        order by CreatedDateTime desc
                        where _WORKFLOWTRACKINGTABLE.WORKFLOWTRACKINGSTATUSTABLE == _WORKFLOWTRACKINGSTATUSTABLE.RECID
                        //&& _WORKFLOWTRACKINGTABLE.createdDateTime == (select maxOf(CreatedDateTime) from _WORKFLOWTRACKINGTABLE
                        //WHERE  _WORKFLOWTRACKINGTABLE.WORKFLOWTRACKINGSTATUSTABLE = _WORKFLOWTRACKINGSTATUSTABLE.RECID).CreatedDateTime
    JOIN* from
                         _WORKFLOWTABLE where  _WORKFLOWTABLE.recid == _WORKFLOWVERSIONTABLE.WORKFLOWTABLE
                         JOIN* from
                         _Purchreqtable where _Purchreqtable.recid == _WORKFLOWTRACKINGSTATUSTABLE.CONTEXTRECID
                        JOIN* from
                        _purchRFQCaseTable where _Purchreqtable.RFQCaseId = _purchRFQCaseTable.RFQCaseId
                        JOIN* from
                        _purchRFQTable where _purchRFQTable.RFQCaseId = _purchRFQCaseTable.RFQCaseId
                        JOIN* from
                        _PurchRFQLine where _PurchRFQLine.RFQId = _purchRFQTable.RFQId
                        JOIN* from
                         _HcmWorker where _HcmWorker.recid == _Purchreqtable.ORIGINATOR
                         JOIN* from
                         _DIRPARTYTABLE where  _DIRPARTYTABLE.recid == _HcmWorker.PERSON
                        && _WORKFLOWTABLE.TEMPLATENAME == 'PurchReqReview'
       /*
where _WORKFLOWTRACKINGSTATUSTABLE.WORKFLOWVERSIONTABLE == _WORKFLOWTRACKINGTABLE.recId
&&    _WORKFLOWTRACKINGTABLE.WORKFLOWTRACKINGSTATUSTABLE == _WORKFLOWTRACKINGSTATUSTABLE.RECID
&&    _WORKFLOWTABLE.recid == _WORKFLOWVERSIONTABLE.WORKFLOWTABLE
&&    _Purchreqtable.recid == _WORKFLOWTRACKINGSTATUSTABLE.CONTEXTRECID
&&    _HcmWorker.recid == _Purchreqtable.ORIGINATOR
&&    _DIRPARTYTABLE.recid == _HcmWorker.PERSON*/
       //where    _WORKFLOWTABLE.TEMPLATENAME == 'PurchReqReview'
    {

        info(_WORKFLOWTRACKINGTABLE.User);
        TmpList.purchReqId = _Purchreqtable.PurchReqId;
        TmpList.purchReqName = _Purchreqtable.purchReqName;
        TmpList.PurchId = _Purchreqtable.PurchId();
        TmpList.RFQCaseId = _Purchreqtable.RFQCaseId();
        // TmpList.Reason = resultSet.getString(5);
        TmpList.Originator = _DIRPARTYTABLE.Name;
        //     TmpList.createdDate =  _Purchreqtable.createdDate();
        //     TmpList.WorkflowStatus =  _WORKFLOWTRACKINGSTATUSTABLE.TrackingStatus;
        // TmpList.ReqStatus =  resultSet.getString(9);
        TmpList.Currency = _Purchreqtable.Currency();
        TmpList.Discount = _Purchreqtable.Discount() * TmpList.QTY;
        TmpList.TotalAmount = _Purchreqtable.SumAmount();
        TmpList.LastActionTime = _WORKFLOWTRACKINGSTATUSTABLE.createdDateTime;

        TmpList.QTY = _PurchRFQLine.QTYOrdered;
        //  TmpList     _WORKFLOWTRACKINGSTATUSTABLE.user
        TmpList.insert();

    }


    Datasource1_ds.executeQuery();
    Datasource1_ds.refresh();
    */
    /*
     Conn = new Connection();
 Stmt = Conn.createStatement();
 //R =Stmt.executeQuery('select * from V_PRLIST');
    sql = 'select * from dbo.V_PRLIST';
permission = new SqlStatementExecutePermission(sql);
conn = new Connection();
permission = new SqlStatementExecutePermission(sql);
permission.assert();
    R =Stmt.executeQuery(sql);*/
    //conn.createStatement().executeUpdate(sql);
    // the permissions needs to be reverted back to original condition.
    simpleProgress = SysOperationProgress::newGeneral(#aviUpdate, 'Extracting PR List',100);
_PRList = new PRList();
    R = _PRList.getPRList();
    while (R.next())
    {
        //print R.getString(1);
        TmpList.clear();
        TmpList.purchReqId = R.getString(1);
        TmpList.purchReqName = R.getString(2);
        TmpList.Vendor = R.getString(3);
        TmpList.PurchId = R.getString(4);
        TmpList.RFQCaseId = R.getString(5);
        TmpList.Reason = R.getString(6);
        TmpList.Currency = R.getString(7);
        TmpList.TotalAmount = str2num(R.getString(8));
        TmpList.Discount = str2num(R.getString(9));
        TmpList.ReqStatus = R.getString(10);
        TmpList.Originator = R.getString(11);

        TmpList.strCreatedDate = R.getString(12);
        TmpList.WorkflowStatus = R.getString(13);
        TmpList.CurrentWorkflowUser = R.getString(14);
        //info(R.getString(14));
        TmpList.strLastAction = R.getString(15);
        //TmpList     _WORKFLOWTRACKINGSTATUSTABLE.user
        purchRFQCaseTable = purchRFQCaseTable::find(strLTrim(TmpList.RFQCaseId));
        _Purchreqtable = PurchReqTable::findPurchReqId(TmpList.purchReqId);
        TmpList.RFQCreatedBy = Dirpersonuser::find(purchRFQCaseTable.createdBy).userName();
        if (purchRFQCaseTable.createdDateTime && _Purchreqtable.SubmittedDateTime && purchRFQCaseTable)
        {
            TmpList.SpanHours = DateTimeUtil::getDifference(purchRFQCaseTable.createdDateTime, _Purchreqtable.SubmittedDateTime) / 3600;
        }
        if (TmpList.PurchId)
        {
            TmpList.PurchStatus = PurchTable::find(TmpList.PurchId).PurchStatus;
        }

        TmpList.QTY = R.getString(18);
        TmpList.insert();
        i++;
        simpleProgress.incCount();
        simpleprogress.setText(strfmt("Lines imported: %1", i));
        sleep(10);

    }
    endLengthyOperation();

    Datasource1_ds.executeQuery();
    Datasource1_ds.refresh();
}