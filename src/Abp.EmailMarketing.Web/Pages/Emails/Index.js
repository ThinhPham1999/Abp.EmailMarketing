$(function () {
    var l = abp.localization.getResource('EmailMarketing');
    var createModal = new abp.ModalManager(abp.appPath + 'Emails/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Emails/EditModal');

    var dataTable = $('#EmailsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            filter: true,
            "searching": true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(abp.emailMarketing.emails.email.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible:
                                        abp.auth.isGranted('EmailMarketing.Emails.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible:
                                        abp.auth.isGranted('EmailMarketing.Emails.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'EmailDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        abp.emailMarketing.emails.email
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Email'),
                    data: "emailString"
                },
                {
                    title: l('Password'),
                    data: "password"
                },
                {
                    title: l('Order'),
                    data: "order"
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

    $('#NewEmailButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
