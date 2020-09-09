using IDCOLAdvanceModule.Model.EntityModels.Base;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.EntityModels.Notification;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class RequisitionApprovalTicket : ApprovalTicket
    {
        public RequisitionApprovalTicket()
        {
            TicketTypeId = (long)TicketTypeEnum.Requisition;
        }

        public long AdvanceRequisitionHeaderId { get; set; }
        public AdvanceRequisitionHeader AdvanceRequisitionHeader { get; set; }

        [NotMapped]
        public virtual IEnumerable<RequisitionApprovalTracker> RequisitionApprovalTrackers
        {
            get
            {
                if (ApprovalTrackers == null)
                {
                    return null;
                }
                return ApprovalTrackers.Cast<RequisitionApprovalTracker>();
            }
            set
            {
                if (value == null)
                {
                    ApprovalTrackers = null;
                }
                else
                {
                    ApprovalTrackers = new List<ApprovalTracker>(value);
                }
            }
        }

        public override ApprovalTracker GetTrackerInstance()
        {
            var requisitionTracker = new RequisitionApprovalTracker()
            {
                ApprovalTicketId = this.Id,
                ApprovalLevelId = this.ApprovalLevelId,
                ApprovalPanelId = this.ApprovalPanelId,
                ApprovalStatusId = this.ApprovalStatusId,
                AuthorizedBy = this.AuthorizedBy,
                AuthorizedOn = this.AuthorizedOn,
                TicketNarration = this.TicketNarration,
                Remarks = this.Remarks
            };
            return requisitionTracker;
        }

        public static RequisitionApprovalTicket GenerateTicket(AdvanceRequisitionHeader requisition, ApprovalLevel approvalLevel, string authorizedBy, ApprovalStatusEnum approvalStatus)
        {
            var ticket = new RequisitionApprovalTicket()
            {
                AdvanceRequisitionHeaderId = requisition.Id,
                RequesterUserName = requisition.RequesterUserName,
                ApprovalLevelId = approvalLevel.Id,
                ApprovalPanelId = approvalLevel.ApprovalPanelId,
                ApprovalStatusId = (long)approvalStatus,
                ApprovalStatusEnum = approvalStatus,
                AuthorizedBy = authorizedBy,
                AuthorizedOn = DateTime.Now
            };

            ticket.SetTicketNarration();
            ticket.SetTrackerInstance();

            return ticket;
        }

        public override void SetTicketNarration(string message="")
        {
            var approvalLevelNarration = string.Empty;
            var authorizedByNarration = AuthorizedBy;
            if (ApprovalLevel != null)
            {
                approvalLevelNarration = ", On " + ApprovalLevel.Name + " level";
            }
            if (ApprovalStatusEnum == ApprovalStatusEnum.ApprovalInitiated ||
                ApprovalStatusEnum == ApprovalStatusEnum.WaitingForApproval ||
                ApprovalStatusEnum == ApprovalStatusEnum.ApprovalSkipped)
            {
                authorizedByNarration = "Advance Module";
            }
            TicketNarration = "Requisition set to " + ApprovalStatusEnum.ToString() + approvalLevelNarration + ", By " + authorizedByNarration + ", On " +
                              AuthorizedOn.ToString("dd MMM, yyyy hh:mm tt");
        }

        public override void UpdateTicketWithTracker(ApprovalStatusEnum requisitionStatusEnum)
        {
            ApprovalStatusEnum = requisitionStatusEnum;
            ApprovalStatusId = (long)requisitionStatusEnum;
            AuthorizedOn = DateTime.Now;
            SetTicketNarration();
            SetTrackerInstance();
        }

        public override bool SetTrackerInstance()
        {
            var tracker = GetTrackerInstance() as RequisitionApprovalTracker;
            if (this.RequisitionApprovalTrackers == null)
            {
                this.RequisitionApprovalTrackers = new List<RequisitionApprovalTracker> { tracker };
            }
            else
            {
                ApprovalTrackers.Add(tracker);
            }

            return true;
        }

        public override void UpdateTicketWithTracker(ApprovalStatusEnum approvalStatus, string approveByUserName)
        {
            AuthorizedBy = approveByUserName;
            UpdateTicketWithTracker(approvalStatus);
        }

        public override string GetNotificationMessage(UserTable requesterInfo, UserTable approvalAuthorityInfo)
        {
            var message = string.Empty;
            if (ApprovalStatusId == (long)ApprovalStatusEnum.WaitingForApproval)
            {
                message = "Requisition# " + AdvanceRequisitionHeader.RequisitionNo + ", Requested By " + requesterInfo.FullName + ", is waiting for your approval.";
            }
            else if (ApprovalStatusId == (long)ApprovalStatusEnum.Approved)
            {
                message = "Requisition# " + AdvanceRequisitionHeader.RequisitionNo + ", has been Approved By " + approvalAuthorityInfo.FullName + ", on " + ApprovalLevel.Name + " level.";
            }
            else if (ApprovalStatusId == (long)ApprovalStatusEnum.Rejected)
            {
                message = "Requisition# " + AdvanceRequisitionHeader.RequisitionNo + ", Requested By " + requesterInfo.FullName + ", has been Rejected By " + approvalAuthorityInfo.FullName + ", on " + ApprovalLevel.Name + " level.";
            }
            else if (ApprovalStatusId == (long)ApprovalStatusEnum.Reverted)
            {
                message = "Requisition# " + AdvanceRequisitionHeader.RequisitionNo + ", Requested By " + requesterInfo.FullName + ", has been Reverted By " + approvalAuthorityInfo.FullName + ", on " + ApprovalLevel.Name + " level.";
            }
            return message;
        }

        public override ICollection<ApplicationNotification> GetNotification(IEnumerable<Advance_VW_GetApprovalLevelMember> notifyingUserList, UserTable requesterInfo, UserTable approvalAuthorityInfo)
        {
            List<ApplicationNotification> aplicationNotifications = new List<ApplicationNotification>();
            foreach (var user in notifyingUserList)
            {
                var requisitionNotification = new RequisitionNotification()
                {
                    Message = GetNotificationMessage(requesterInfo, approvalAuthorityInfo),
                    RequisitionId = AdvanceRequisitionHeaderId,
                    NotificationDate = DateTime.Now,
                    ToUserName = user.EmployeeUserName,
                    TicketStatusId = ApprovalStatusId,
                    ApprovalTicketId = Id,
                    ApprovalLevelId = ApprovalLevelId
                };

                aplicationNotifications.Add(requisitionNotification);
            }
            return aplicationNotifications;
        }

        public override ICollection<EmailNotification> GetEmail(IEnumerable<Advance_VW_GetApprovalLevelMember> notifyingUserList, UserTable requesterInfo, UserTable approvalAuthorityInfo)
        {
            ICollection<EmailNotification> emailNotifications = new List<EmailNotification>();
            foreach (var user in notifyingUserList)
            {
                var emailNotification = new EmailNotification
                {
                    From = Utility.Utility.EmailFrom,
                    To = user.EmployeeEmail,
                    ToUserName = user.EmployeeUserName,
                    Subject = GetEmailSubject(),
                    MessageBody = GetEmailMessageBody(requesterInfo, approvalAuthorityInfo),
                    MessageContentHtml = GetEmailMessageHtmlBody(requesterInfo, approvalAuthorityInfo),
                    CreatedDate = DateTime.Now,
                    ApprovalLevelId = ApprovalLevelId,
                    ApprovalTicketId = Id,
                    TicketStatusId = ApprovalStatusId
                };

                emailNotifications.Add(emailNotification);
            }
            return emailNotifications;
        }

        public override string GetEmailSubject()
        {
            var subject = "[Advance Module] Requisition# " + AdvanceRequisitionHeader.RequisitionNo;
            if (ApprovalStatusId == (long)ApprovalStatusEnum.WaitingForApproval)
            {
                subject += " is waiting for your approval";
            }
            else if (ApprovalStatusId == (long)ApprovalStatusEnum.Approved)
            {
                subject += " has been approved on " + ApprovalLevel.Name + " level";
            }
            else if (ApprovalStatusId == (long)ApprovalStatusEnum.Rejected)
            {
                subject += " has been rejected on " + ApprovalLevel.Name + " level";
            }
            else if (ApprovalStatusId == (long)ApprovalStatusEnum.Reverted)
            {
                subject += " has been reverted on " + ApprovalLevel.Name + " level";
            }
            return subject;
        }

        public override string GetEmailMessageBody(UserTable requesterInfo, UserTable approvalAuthorityInfo)
        {
            var message = "Dear Sir/Madam," + Environment.NewLine + "Greetings from Advance Module." + Environment.NewLine;
            if (ApprovalStatusId == (long)ApprovalStatusEnum.WaitingForApproval)
            {
                message += "Requisition# " + AdvanceRequisitionHeader.RequisitionNo + ", requested by " + requesterInfo.FullName + ", is waiting for your approval.";
            }
            else if (ApprovalStatusId == (long)ApprovalStatusEnum.Approved)
            {
                message += "Requisition# " + AdvanceRequisitionHeader.RequisitionNo + " has been approved by " + approvalAuthorityInfo.FullName + ", at " + ApprovalLevel.Name + " level on " + AuthorizedOn.ToString("dd MMM, yyyy hh:mm tt") + ".";
            }
            else if (ApprovalStatusId == (long)ApprovalStatusEnum.Rejected)
            {
                message += "Requisition# " + AdvanceRequisitionHeader.RequisitionNo + ", requested by " + requesterInfo.FullName + ", has been rejected by " + approvalAuthorityInfo.FullName + ", at " + ApprovalLevel.Name + " level on " + AuthorizedOn.ToString("dd MMM, yyyy hh:mm tt") + ".";
            }
            else if (ApprovalStatusId == (long)ApprovalStatusEnum.Reverted)
            {
                message += "Requisition# " + AdvanceRequisitionHeader.RequisitionNo + ", requested by " + requesterInfo.FullName + ", has been reverted by " + approvalAuthorityInfo.FullName + ", at " + ApprovalLevel.Name + " level on " + AuthorizedOn.ToString("dd MMM, yyyy hh:mm tt") + ".";
            }
            string moreInfo =
                "You can find the information against this notification in your Advance Module notification section.";

            string disclaimer = "Disclaimer: This is an auto generated email from Advance Module. You don't have to reply this email.";

            return message + Environment.NewLine + moreInfo + "Thanks." + Environment.NewLine + "[" + disclaimer + "]";
        }

        public override string GetEmailMessageHtmlBody(UserTable requesterInfo, UserTable approvalAuthorityInfo)
        {
            var message = "<p>Dear Sir/Madam,</p></p>Greetings from <b>Advance Module</b>.</p>";
            if (ApprovalStatusId == (long)ApprovalStatusEnum.WaitingForApproval)
            {
                message += "<p>Requisition# <b>" + AdvanceRequisitionHeader.RequisitionNo + "</b>, requested by <b>" + requesterInfo.FullName + "</b>, is waiting for your approval.</p>";
            }
            else if (ApprovalStatusId == (long)ApprovalStatusEnum.Approved)
            {
                message += "<p>Requisition# <b>" + AdvanceRequisitionHeader.RequisitionNo + "</b> has been approved by <b>" + approvalAuthorityInfo.FullName + "</b>, at <b>" + ApprovalLevel.Name + "</b> level on " + AuthorizedOn.ToString("dd MMM, yyyy hh:mm tt") + ".</p>";
            }
            else if (ApprovalStatusId == (long)ApprovalStatusEnum.Rejected)
            {
                message += "<p>Requisition# <b>" + AdvanceRequisitionHeader.RequisitionNo + "</b>, requested by <b>" + requesterInfo.FullName + "</b>, has been rejected by <b>" + approvalAuthorityInfo.FullName + "</b>, at <b>" + ApprovalLevel.Name + "</b> level on " + AuthorizedOn.ToString("dd MMM, yyyy hh:mm tt") + ".</p>";
            }
            else if (ApprovalStatusId == (long)ApprovalStatusEnum.Reverted)
            {
                message += "<p>Requisition# <b>" + AdvanceRequisitionHeader.RequisitionNo + "</b>, requested by <b>" + requesterInfo.FullName + "</b>, has been reverted by <b>" + approvalAuthorityInfo.FullName + "</b>, at <b>" + ApprovalLevel.Name + "</b> level on " + AuthorizedOn.ToString("dd MMM, yyyy hh:mm tt") + ".</p>";
            }
            string moreInfo =
                "You can find the information against this notification in your <b>Advance Module notification section</b>.";

            string disclaimer = "Disclaimer: This is an auto generated email from Advance Module. You don't have to reply this email.";

            return message + "<p>" + moreInfo + "</p><p>Thanks.</p><hr/><em>[" + disclaimer + "]</em>";
        }

        public override string GetCreatorUserName()
        {
            if (AdvanceRequisitionHeader == null)
            {
                return null;
            }
            return AdvanceRequisitionHeader.CreatedBy;
        }
    }
}
