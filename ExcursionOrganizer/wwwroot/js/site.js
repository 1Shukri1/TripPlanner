//javascript функции за модален прозорец
function confirmUnsubscribe() {
    Swal.fire({
        title: 'Сигурен ли сте?',
        text: "Наистина ли искате да се отпишете от екскурзията?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Да!',
        cancelButtonText: 'Не'
    }).then((result) => {
        if (result.isConfirmed) {
            document.getElementById('unsubscribeForm').submit();
        }
    });
}

function confirmDelete() {
    Swal.fire({
        title: 'Сигурен ли сте?',
        text: "Наистина ли искате да изтриете тази екскурзия?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Да, изтрий!',
        cancelButtonText: 'Отказ'
    }).then((result) => {
        if (result.isConfirmed) {
            document.getElementById('deleteForm').submit();
        }
    });
}
