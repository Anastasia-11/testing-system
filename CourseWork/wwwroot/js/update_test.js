function removeQuestion(index) {
    Questions.splice(index, 1);
    document.getElementById("QuestionsLimit").setAttribute("max", Questions.length);
    redrawQuestions();
}

function removeAnswer(questionIndex, answerIndex) {
    Questions[questionIndex].Answers.splice(answerIndex, 1);
    for (let i = 0; i < Questions[questionIndex].Answers.length; i++) {
        Questions[questionIndex].Answers[i].Number = i;
    }
    redrawQuestions();
}

function addQuestion() {
    let question_type = getSelectedType();
    let questionId = createGuid();

    Questions.push({
        Id: questionId,
        TestId: getTestId(),
        Type: question_type,
        Number: Questions.length,
        Text: "",
        Cost: 1,
        Answers: []
    })

    Questions[Questions.length - 1].Answers.push({
        Id: createGuid(),
        QuestionId: questionId,
        IsCorrect: false,
        Response: "",
        CorrectResponse: "",
        Number: 0
    })

    document.getElementById("QuestionsLimit").setAttribute("max", Questions.length);
    redrawQuestions();
}

function addAnswer(questionIndex) {
    let questionId = Questions[questionIndex].Id;

    Questions[questionIndex].Answers.push({
        Id: createGuid(),
        QuestionId: questionId,
        IsCorrect: false,
        Response: "",
        CorrectResponse: "",
        Number: Questions[questionIndex].Answers.length
    })

    redrawQuestions();
}

function redrawQuestions() {
    let html = "";

    for (let i = 0; i < Questions.length; i++) {
        html += "<br>";
        html += "<div class=\"form-group\">";
        html += getQuestionText(i);

        switch (Questions[i].Type) {
            case 0:
            case "WithAnswerOptions":
                for (let j = 0; j < Questions[i].Answers.length; j++) {
                    html += getAnswerOptions(i, j);
                }
                html += "<input type=\"button\" class=\"btn btn-outline-primary\" value=\"Добавить ответ\" onclick=\"addAnswer(" + i + ")\">";
                break;
            case 1:
            case "FreeEntry":
                html += getFreeEntryAnswer(i, 0);
                break;
            default:
                alert("wrong question type!");
        }

        html += "<input type=\"button\" class=\"btn btn-outline-danger\" value=\"Удалить вопрос\" onclick=\"removeQuestion(" + i + ")\">";
        html += "</div>";
    }

    let container = document.getElementById("Questions");
    container.innerHTML = html;
}

function getQuestionText(index) {
    let html = "<h6>Вопрос " + (index + 1) + ":</h6>";
    html += "<div class=\"row\">";
    html += "<div class=\"col-md-auto\">";
    html += "<label class=\"control-label\">Баллы за правильный ответ:</label>";
    html += "</div>";
    html += "<div class=\"col-md-auto\">";
    html += "<input type=\"number\" name=\"Questions[" + index + "].Cost\" class=\"form-control\" min=\"1\" max=\"100\" step=\"1\" value=\"" + Questions[index].Cost + "\" oninput=\"onQuestionCostChange(this.value," + index + ")\"/>";
    html += "</div>";
    html += "</div>";
    html += "<input type=\"hidden\" name=\"Questions[" + index + "].Id\" value=\"" + Questions[index].Id + "\">";
    html += "<input type=\"hidden\" name=\"Questions[" + index + "].TestId\" value=\"" + Questions[index].TestId + "\">";
    html += "<input type=\"hidden\" name=\"Questions[" + index + "].Type\" value=\"" + Questions[index].Type + "\">";
    html += "<input type=\"hidden\" name=\"Questions[" + index + "].Number\" value=\"" + index.toString() + "\">";
    html += "<textarea class=\"form-control\" name=\"Questions[" + index + "].Text\" placeholder=\"Введите текст вопроса\" oninput=\"onQuestionTextChange(this.value," + index + ")\">" + Questions[index].Text + "</textarea>";
    return html;
}

function getAnswerOptions(questionIndex, answerIndex) {
    let html = "";
    html += "<div class=\"input-group\">";
    html += "<input type=\"hidden\" name=\"Questions[" + questionIndex + "].Answers[" + answerIndex + "].Id\" value=\"" + Questions[questionIndex].Answers[answerIndex].Id + "\">";
    html += "<input type=\"hidden\" name=\"Questions[" + questionIndex + "].Answers[" + answerIndex + "].QuestionId\" value=\"" + Questions[questionIndex].Answers[answerIndex].QuestionId + "\">";
    html += "<input type=\"hidden\" name=\"Questions[" + questionIndex + "].Answers[" + answerIndex + "].Number\" value=\"" + Questions[questionIndex].Answers[answerIndex].Number + "\">";
    html += "<div class=\"form-check\">";
    html += "<input class=\"form-check-input\" type=\"checkbox\" name=\"Questions[" + questionIndex + "].Answers[" + answerIndex + "].IsCorrect\" onclick=\"changeAnswerState(this," + questionIndex + "," + answerIndex +")\"" + (Questions[questionIndex].Answers[answerIndex].IsCorrect ? "checked value=\"true\"": "value=\"false\"") + ">";
    html += "</div>";
    html += "<input type=\"text\" class=\"form-control\" id=\"Questions[" + questionIndex + "].Answers[" + answerIndex + "].Response\" name=\"Questions[" + questionIndex + "].Answers[" + answerIndex + "].Response\" value=\"" + Questions[questionIndex].Answers[answerIndex].Response + "\" placeholder=\"Введите текст ответа\" oninput=\"onAnswerResponseChange(this.value," + questionIndex + "," + answerIndex +")\">";
    html += "<input type=\"button\" class=\"btn btn-outline-danger\" value=\"Удалить ответ\" onclick=\"removeAnswer(" + questionIndex + "," + answerIndex +")\">";
    html += "</div>";
    return html;
}

function getFreeEntryAnswer(questionIndex, answerIndex) {
    let html = "";
    html += "<input type=\"hidden\" name=\"Questions[" + questionIndex + "].Answers[" + answerIndex + "].Id\" value=\"" + Questions[questionIndex].Answers[answerIndex].Id + "\">";
    html += "<input type=\"hidden\" class=\"form-control\" name=\"Questions[" + questionIndex + "].Answers[" + answerIndex + "].QuestionId\" value=\"" + Questions[questionIndex].Answers[answerIndex].QuestionId + "\">";
    html += "<input type=\"hidden\" name=\"Questions[" + questionIndex + "].Answers[" + answerIndex + "].Number\" value=\"" + Questions[questionIndex].Answers[answerIndex].Number + "\">";
    html += "<input type=\"text\" class=\"form-control\" name=\"Questions[" + questionIndex + "].Answers[" + answerIndex + "].CorrectResponse\" value=\"" + Questions[questionIndex].Answers[answerIndex].CorrectResponse + "\" placeholder=\"Введите текст ответа\" oninput=\"onAnswerCorrectResponseChange(this.value," + questionIndex + "," + answerIndex +")\">";
    return html;
}

function changeAnswerState(checkbox, questionIndex, answerIndex) {
    checkbox.setAttribute("value", checkbox.checked);
    Questions[questionIndex].Answers[answerIndex].IsCorrect = checkbox.checked;
}

function getSelectedType() {
    return document.getElementById("questionType").value;
}

function getTestId() {
    return document.getElementById("testId").value;
}

function createGuid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
}

function onQuestionTextChange(text, index) {
    Questions[index].Text = text;
}

function onQuestionCostChange(value, index) {
    Questions[index].Cost = value;
}

function onAnswerCorrectResponseChange(text, questionIndex, answerIndex) {
    Questions[questionIndex].Answers[answerIndex].CorrectResponse = text;
}

function onAnswerResponseChange(text, questionIndex, answerIndex) {
    Questions[questionIndex].Answers[answerIndex].Response = text;
}