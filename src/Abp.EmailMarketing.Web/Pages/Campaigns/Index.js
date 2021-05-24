$(function () {
    var l = abp.localization.getResource('EmailMarketing');
    var createModal = new abp.ModalManager(abp.appPath + 'Campaigns/CreateCampaign');

    var dataTable = $('#CampaignsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            filter: true,
            "searching": true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(abp.emailMarketing.campaigns.campaign.getList),
            columnDefs: [
                /*{
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    //visible: abp.auth.isGranted('EmailMarketing.Contacts.Edit'), // Check for the Permission
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    //visible: abp.auth.isGranted('EmailMarketing.Contacts.Delete'), // Check for the Permission
                                    confirmMessage: function (data) {
                                        return l('CampaignDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        abp.emailMarketing.campaigns.campaign
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }

                            ]
                    }
                },*/
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Description'),
                    data: "description"
                }
                ,
                {
                    title: l('Title'),
                    data: "title"
                },
                {
                    title: l('Schedule'),
                    data: "schedule",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
                    }
                },
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });
    $('#NewCampaignButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
