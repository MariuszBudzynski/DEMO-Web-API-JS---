const saveButton = document.querySelector("#btnSave");
const titleInput = document.querySelector("#title");
const descriptionIput = document.querySelector("#description");
const notesContainer = document.querySelector(".notes_container");
const deleteButton = document.querySelector("#btnDelete");

getAllNotes();

saveButton.addEventListener("click", function () {
  const id = saveButton.dataset.id;
  if (id) {
    updateNote(id, titleInput.value, descriptionIput.value);
  } else {
    addNote(titleInput.value, descriptionIput.value);
  }
});

deleteButton.addEventListener("click", function () {
  const id = deleteButton.dataset.id;
  deleteNote(id);
});

function populateForm(id) {
  getNoteById(id);
}

function clearForm() {
  deleteButton.classList.add("hidden");
  titleInput.value = "";
  descriptionIput.value = "";
}

function displayNoteInForm(note) {
  titleInput.value = note.title;
  descriptionIput.value = note.description;
  deleteButton.classList.remove("hidden");
  deleteButton.setAttribute("data-id", note.id);
  saveButton.setAttribute("data-id", note.id);
}

//function that talks to API
function addNote(title, description) {
  //body request
  const body = {
    title: title,
    description: description,
    isVisible: true,
  };

  //HTTP requests function
  fetch("https://localhost:7116/api/Notes", {
    method: "POST", //type of the request
    body: JSON.stringify(body), //coverting to JSON
    headers: {
      "content-type": "application/json", //informs the server that the request will be in JSON format
    },
  })
    .then((data) => data.json()) //After receiving the response from the server, this line of code converts the response body into JSON format
    .then(() => {
      clearForm();
      getAllNotes();
    });
}

//function will generate the HTML code on front
function displayNotes(notes) {
  let notesToUpdate = "";

  notes.forEach((note) => {
    const noteElemnt = `
    <div class="note" data-id="${note.id}">
    <h3>${note.title}</h3>
    <p>${note.description}</p>
  </div>`;
    notesToUpdate += noteElemnt;
  });
  notesContainer.innerHTML = notesToUpdate;

  document.querySelectorAll(".note").forEach((note) => {
    note.addEventListener("click", function () {
      populateForm(note.dataset.id);
    });
  });
}

function getAllNotes() {
  fetch("https://localhost:7116/api/Notes")
    .then((data) => data.json())
    .then((response) => displayNotes(response));
}

function getNoteById(id) {
  fetch(`https://localhost:7116/api/Notes/${id}`)
    .then((data) => data.json())
    .then((response) => displayNoteInForm(response));
}

function deleteNote(id) {
  fetch(`https://localhost:7116/api/Notes/${id}`, {
    method: "DELETE",
    headers: {
      "content-type": "application/json",
    },
  }).then(() => {
    clearForm();
    getAllNotes();
  }); //we are not using the data.json() as the DELETE does not return any data just status code
}

function updateNote(id, title, description) {
  const body = {
    title: title,
    description: description,
    isVisible: true,
  };

  fetch(`https://localhost:7116/api/Notes/${id}`, {
    method: "PUT",
    body: JSON.stringify(body),
    headers: {
      "content-type": "application/json",
    },
  })
    .then((data) => data.json())
    .then(() => {
      clearForm();
      getAllNotes();
    });
}
