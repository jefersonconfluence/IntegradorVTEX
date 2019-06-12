using IntegradorAticOs.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace IntegradorAticOs.DAL
{
    public class DraftDAL : BaseUDO
    {
        private SAPbobsCOM.Company oCompany;

        internal DraftDAL(SAPbobsCOM.Company company)
            : base(company)
        {
            this.oCompany = company;
        }

        public string InsertDraft(DraftDocumentEntity draft, out string messageError)
        {
            int addDraftNumber = 0;

            LogDAL _log = new LogDAL();

            try
            {

                SAPbobsCOM.Documents oDocDraft = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);

                oDocDraft.CardCode = draft.CardCode;
                oDocDraft.DocObjectCode = (SAPbobsCOM.BoObjectTypes)13;
                oDocDraft.BPL_IDAssignedToInvoice = Convert.ToInt16(draft.BPLId);

                oDocDraft.OpeningRemarks = draft.OpeningRemarks;
                oDocDraft.ClosingRemarks = draft.ClosingRemarks;
                oDocDraft.Comments = draft.Comments;

                oDocDraft.DocDate = DateTime.ParseExact(draft.DocDate.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

                if (!string.IsNullOrEmpty(draft.DocDueDate))
                    oDocDraft.DocDueDate = DateTime.ParseExact(draft.DocDueDate.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

                oDocDraft.DocCurrency = draft.DocCurrency;
                oDocDraft.DocRate = draft.DocRate;

                oDocDraft.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;

                oDocDraft.UserFields.Fields.Item("U_UPP_N_OS").Value = draft.N_Os;
                oDocDraft.UserFields.Fields.Item("U_Identificacao").Value = draft.DocCode;
                oDocDraft.UserFields.Fields.Item("U_UPBL").Value = draft.U_UPBL;
                oDocDraft.UserFields.Fields.Item("U_UPAticDraft").Value = draft.U_UPAticDraft;
                int _first = 0;

                foreach (DraftLineEntity _line in draft.Document_Line)
                {
                    oDocDraft.Lines.ItemCode = _line.ItemCode;
                    oDocDraft.Lines.CostingCode = _line.CostingCode;
                    oDocDraft.Lines.Currency = _line.Currency;
                    oDocDraft.Lines.Quantity = _line.Quantity;
                    oDocDraft.Lines.UnitPrice = _line.UnitPrice;
                    oDocDraft.Lines.DiscountPercent = _line.DiscountPercent;
                    oDocDraft.Lines.Usage = _line.Usage;
                    oDocDraft.Lines.WarehouseCode = _line.WharehouseCode;
                    oDocDraft.Lines.Add();
                    oDocDraft.Lines.SetCurrentLine(oDocDraft.Lines.Count - 1);

                    _first++;
                }

                addDraftNumber = oDocDraft.Add();

            }
            catch (Exception ex)
            {
                _log.WriteEntry("InsertDraft exception: " + ex.Message);
                throw;
            }

            if (addDraftNumber != 0)
            {
                messageError = oCompany.GetLastErrorDescription();

                _log.WriteEntry("InsertDraft error SAP: " + messageError);

                return "-3";
            }
            else
            {
                draft.Sketch_Invoice = oCompany.GetNewObjectKey();
                messageError = "";
                return "0";
            }
        }

        public string InsertSplitedDraft(DraftDocumentEntity draft, List<DraftLineEntity> draftLines)
        {
            int addDraftNumber = 0;

            try
            {
                SAPbobsCOM.Documents oDocDraft = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);

                oDocDraft.CardCode = draft.CardCode;
                oDocDraft.DocObjectCode = (SAPbobsCOM.BoObjectTypes)13;
                oDocDraft.BPL_IDAssignedToInvoice = Convert.ToInt16(draft.BPLId);

                oDocDraft.OpeningRemarks = draft.OpeningRemarks;
                oDocDraft.ClosingRemarks = draft.ClosingRemarks;
                oDocDraft.Comments = draft.Comments;

                oDocDraft.DocDate = DateTime.ParseExact(draft.DocDate.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

                if (!string.IsNullOrEmpty(draft.DocDueDate))
                    oDocDraft.DocDueDate = DateTime.ParseExact(draft.DocDueDate.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

                oDocDraft.DocCurrency = draft.DocCurrency;
                oDocDraft.DocRate = draft.DocRate;

                oDocDraft.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;

                oDocDraft.UserFields.Fields.Item("U_UPP_N_OS").Value = draft.N_Os;
                oDocDraft.UserFields.Fields.Item("U_Identificacao").Value = draft.DocCode;
                oDocDraft.UserFields.Fields.Item("U_UPBL").Value = draft.U_UPBL;
                oDocDraft.UserFields.Fields.Item("U_UPAticDraft").Value = draft.U_UPAticDraft;

                int _first = 0;

                foreach (DraftLineEntity _line in draftLines)
                {

                    oDocDraft.Lines.ItemCode = _line.ItemCode;
                    oDocDraft.Lines.CostingCode = _line.CostingCode;
                    oDocDraft.Lines.Currency = _line.Currency;
                    oDocDraft.Lines.Quantity = _line.Quantity;
                    oDocDraft.Lines.UnitPrice = _line.UnitPrice;
                    oDocDraft.Lines.DiscountPercent = _line.DiscountPercent;
                    oDocDraft.Lines.Usage = _line.Usage;
                    oDocDraft.Lines.WarehouseCode = _line.WharehouseCode;
                    oDocDraft.Lines.Add();
                    oDocDraft.Lines.SetCurrentLine(oDocDraft.Lines.Count - 1);

                    _first++;
                }

                addDraftNumber = oDocDraft.Add();

            }
            catch (Exception ex)
            {
                throw;
            }

            if (addDraftNumber != 0)
            {
                var erro = oCompany.GetLastErrorDescription();

                LogDAL _log = new LogDAL();
                _log.WriteEntry("InsertDraft error SAP: " + erro);

                return "-3";
            }
            else
            {
                draft.Sketch_Invoice = oCompany.GetNewObjectKey();
                return "0";
            }
        }
    }
}
