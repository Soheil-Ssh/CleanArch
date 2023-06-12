$(() => {
    //#region Congif tooltip

    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    const tooltipList = [...tooltipTriggerList].map((tooltipTriggerEl) => new bootstrap.Tooltip(tooltipTriggerEl));

    //#endregion

    //#region Delete buttons

    const deleteBtn = $('a[data-delete="true"]');

    deleteBtn.on('click', (e) => {
        // target
        const thisTarget = $(e.currentTarget);
        // get target href
        const url = thisTarget.attr('data-delete-url');
        // get target id
        const id = thisTarget.attr('data-delete-id');

        // check is not undefined url and id
        if (url == undefined || id == undefined) {
            // show error toast
            toastr.error('Error! Cannot be deleted.');
            return;
        }

        // show sweet alert
        Swal.fire({
            icon: 'warning',
            text: 'Are you sure to delete?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
            allowOutsideClick: false,
            customClass: {
                confirmButton: 'btn btn-danger',
                cancelButton: 'btn btn-light',
            },
            preConfirm: (response) => {
                return new Promise((resolve, reject) => {
                    // send ajax request
                    $.ajax({
                        url: `${url}/${id}`,
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        method: 'DELETE',
                    })
                        .done((data) => {
                            resolve(data);
                        })
                        .fail((xhr, textStatus) => {
                            reject(xhr);
                        });
                });
            },
        })
            .then((result) => {
                if (result.dismiss !== 'cancel') {
                    if (result.value.succeeded) {
                        // success notification
                        toastr.success(result.value.message);

                        // fade and remove row
                        $(`#${id}`).fadeOut('slow', (e) => {
                            $(`#${id}`).remove();
                        });
                    } else {
                        // error notification
                        toastr.error(result.value.message);
                    }
                }
            })
            .catch((xhr) => {
                // log error
                console.error(xhr);

                // error notification
                toastr.error('خطای 500!');

                // close sweet alert
                Swal.close();
            });
    });

    //#endregion
});
