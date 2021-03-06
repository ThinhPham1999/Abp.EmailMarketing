$(function () {
    var l = abp.localization.getResource('EmailMarketing');
    var createModal = new abp.ModalManager(abp.appPath + 'GroupContacts/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'GroupContacts/EditModal');
    var viewContactModal = new abp.ModalManager(abp.appPath + 'GroupContacts/ContactView');


    var dataTable = $('#GroupsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            filter: true,
            "searching": true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(abp.emailMarketing.groupContacts.group.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible:
                                        abp.auth.isGranted('EmailMarketing.Groups.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible:
                                        abp.auth.isGranted('EmailMarketing.Groups.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'GroupDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        abp.emailMarketing.groupContacts.group
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                },
                                {
                                    text: l('View Contact'),
                                    visible:
                                        abp.auth.isGranted('EmailMarketing.Groups.Edit'),
                                    action: function (data) {
                                        viewContactModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Import Contact'),
                                    visible:
                                        abp.auth.isGranted('EmailMarketing.Groups.Edit'),
                                    action: function (data) {
                                        window.location = '/GroupContacts/ImportContact?id=' + data.record.id;
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Description'),
                    data: "description"
                }
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });


    $('#NewGroupButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    
});
