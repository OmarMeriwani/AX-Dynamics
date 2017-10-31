using System;

public class Class1
{
	public Class1()
	{
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
            ;
# avifiles

            startLengthyOperation();
            delete_from TmpList;

        
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
    }
}
