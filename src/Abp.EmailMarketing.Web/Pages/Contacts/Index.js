$(function () {
    var l = abp.localization.getResource('EmailMarketing');
    var createModal = new abp.ModalManager(abp.appPath + 'Contacts/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Contacts/EditModal');
    

    var dataTable = $('#ContactsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(abp.emailMarketing.contacts.contact.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('EmailMarketing.Contacts.Edit'), // Check for the Permission
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('EmailMarketing.Contacts.Delete'), // Check for the Permission
                                    confirmMessage: function (data) {
                                        return l('ContactDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        abp.emailMarketing.contacts.contact
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }

                            ]
                    }
                },
                {
                    title: l('Email'),
                    data: "email"
                },
                {
                    title: l('FirstName'),
                    data: "firstName"
                },
                {
                    title: l('LastName'),
                    data: "lastName"
                },
                {
                    title: l('Type'),
                    data: "type",
                    render: function (data) {
                        return l('Enum:ContactType:' + data);
                    }
                },
                {
                    title: l('DateOfBirth'),
                    data: "dateOfBirth",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
                    }
                },
                {
                    title: l('CreationTime'), data: "creationTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
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

    $('#NewContactButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });


    

});
