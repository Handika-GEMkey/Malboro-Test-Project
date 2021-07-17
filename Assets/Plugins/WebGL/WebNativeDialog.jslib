mergeInto(LibraryManager.library, {
    RecheiveData : function(_game_object, _id, _name, _score){
        
        var game_object_name = Pointer_stringify(_game_object);
        var function_name1 = Pointer_stringify(_id);
        var function_name2 = Pointer_stringify(_name);
        var function_name3 = Pointer_stringify(_score);

        var myId = localStorage.getItem("toyotagame_id");
        var myName = localStorage.getItem("toyotagame_name");
        var myScore = localStorage.getItem("toyotagame_score");

        SendMessage(game_object_name, function_name1, myId);
        SendMessage(game_object_name, function_name2, myName);
        SendMessage(game_object_name, function_name3, myScore);
    },

    SendData : function(_id, _name, _score){

        var myId = Pointer_stringify(_id);
        var myName = Pointer_stringify(_name);
        var myScore = _score;

        localStorage.setItem("toyotagame_id", myId);
        localStorage.setItem("toyotagame_name", myName);
        localStorage.setItem("toyotagame_score", myScore);

        window.location.replace("/generate/" + myId);
    },

    /*InitGame : function(_game_object, _function){
        var startButton = document.getElementById("start_button");
        var opening = document.getElementsByClassName("opening")[0];

        var game_object_name = Pointer_stringify(_game_object);
        var function_name = Pointer_stringify(_function);

        startButton.addEventListener("click", function(){
            opening.style.display = "none";
            SendMessage(game_object_name, function_name);
        });
    },*/
});