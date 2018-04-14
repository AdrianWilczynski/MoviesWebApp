let contactFormActionOriginalValue = document.getElementById("contactForm").action;

document.getElementById("sendEmailButton")
    .addEventListener("click",
    function () {
        let subject = contactForm.subject.value;
        let body = contactForm.body.value;

        contactForm.action = contactFormActionOriginalValue;
        contactForm.action = contactForm.action
            .replace('subjectGoesHere', subject)
            .replace('bodyGoesHere', body !== "" ? body : "Treść wiadomości...");
    });