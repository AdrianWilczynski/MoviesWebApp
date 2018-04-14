document.getElementById("sendEmailButton")
    .addEventListener("click",
    function () {
        let contactForm = document.getElementById("contactForm");

        let subject = contactForm.subject;
        let body = contactForm.body;

        let validationErrorForBody = document.getElementById("validationErrorForBody");
        let validationErrorForSubject = document.getElementById("validationErrorForSubject");

        validationErrorForBody.textContent = "";
        validationErrorForSubject.textContent = "";

        if (body.classList.contains('input-validation-error')) {
            body.classList.remove('input-validation-error');
        }
        if (subject.classList.contains('input-validation-error')) {
            subject.classList.remove('input-validation-error');
        }

        if (body.value.length === 0) {
            validationErrorForBody.textContent = 'Treść wiadomości jest wymagana';
            body.classList.add('input-validation-error');

            event.preventDefault();
            event.stopPropagation();
        } else if (body.value.length > 600) {
            validationErrorForBody.textContent = 'Maksymalna liczba znaków w wiadomości to 600';
            body.classList.add('input-validation-error');

            event.preventDefault();
            event.stopPropagation();
        }

        if (subject.value.length === 0) {
            validationErrorForSubject.textContent = 'Temat wiadomości jest wymagany';
            subject.classList.add('input-validation-error');

            event.preventDefault();
            event.stopPropagation();
        } else if (subject.value.length > 50) {
            validationErrorForSubject.textContent = 'Maksymalna liczba znaków w temacie to 50';
            subject.classList.add('input-validation-error');

            event.preventDefault();
            event.stopPropagation();
        }
    })