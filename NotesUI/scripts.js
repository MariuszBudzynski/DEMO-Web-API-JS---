const saveButton = document.querySelector("#btnSave");
const titleInput = document.querySelector("#title");
const descriptionIput = document.querySelector("#description");
const notesContainer = document.querySelector(".notes_container");

getAllNotes();

saveButton.addEventListener("click", function () {
  addNote(titleInput.value, descriptionIput.value);
});

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
    .then((response) => console.log(response));
}

//function will generate the HTML code on front
function displayNotes(notes) {
  notes.forEach((note) => {
    const noteElemnt = `
    <div class="note">
    <h3>${note.title}</h3>
    <p>${note.description}</p>
  </div>`;
    notesContainer.innerHTML += noteElemnt;
  });
}

function getAllNotes() {
  fetch("https://localhost:7116/api/Notes")
    .then((data) => data.json())
    .then((response) => displayNotes(response));
}
