
var app = angular.module('bookStoreApp', ['angularUtils.directives.dirPagination']);
var baseAddress = 'http://localhost:55170/';
var url = "";

//app.config([
//"$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider) {
//    $routeProvider.when("/Author",
//    {
//        templateUrl: "",
//        controller: "AuthorController",
//        title: "Author Page"
//    }).when("/Book",
//    {
//        templateUrl: "",
//        controller: "BookController",
//        title: "Book Page"
//    }).otherwise({
//        redirectTo: "/"
//    });

//    // use the HTML5 History API
//    $locationProvider.html5Mode({
//        enabled: true
//        //requireBase: true,
//        //rewriteLinks: true
//    });
//}
//]);

// This portion responsible for changing title of the page
app.run(['$location', '$rootScope', function ($location, $rootScope) {
    $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {

        if (current.hasOwnProperty('$$route')) {

            $rootScope.title = current.$$route.title;
        }
    });
}]);



app.factory('bookStoreFactory', function ($http) {
    return {
        getUsersList: function () {
            url = baseAddress + "author/list";
            return $http.get(url);
        },
        getUser: function (author) {
            url = baseAddress + "author/getAuthor/" + author.Id;
            return $http.get(url);
        },
        addUser: function (author) {
            url = baseAddress + "author/post";
            return $http.post(url, author);
        },
        deleteUser: function (author) {
            url = baseAddress + "DeleteAuthor/" + author.Id;
            return $http.delete(url);
        },
        updateUser: function (author) {
            url = baseAddress + "author/Update/" + author.Id;
            return $http.put(url, author);
        }
    };
});

app.controller('AuthorController', function PostController($scope, $http, bookStoreFactory) {  //i add $http, for paging
          
    $scope.authors = [];
    $scope.author = null;
    $scope.editMode = false;

    //get User display author when row click
    $scope.get = function () {
        $scope.author = this.author;
        $scope.editMode = true;
        $scope.editText = true;

        console.log('table row click');
        $('#AuthorSaveModel').modal('show');
        //$('#AuthorViewModal').modal('show');
    };

    //get all Users
    $scope.getAll = function () {
        console.log('call data list');     
        bookStoreFactory.getUsersList().success(function (data) {
            console.log(data.list);
            $scope.authors = data.list;
       
        }).error(function (data) {
            $scope.error = "An Error has occured while Loading users! " + data.ExceptionMessage;
        });

       
        //add for pagination
        $scope.sort = function (keyname) {
            $scope.sortKey = keyname;   //set the sortKey to the param passed
            $scope.reverse = !$scope.reverse; //if true make it false and vice versa
        }
    };

    // add User
    $scope.add = function () {
        var currentAuthor = this.author;
        if (currentAuthor != null && currentAuthor.FirstName != null && currentAuthor.Address && currentAuthor.LastName && currentAuthor.ZipCode && currentAuthor.Country && currentAuthor.Initials) {
            bookStoreFactory.addUser(currentAuthor).success(function (data) {

                $scope.addMode = false; // 
                $scope.editText = false;
                currentAuthor.Id = data.id;

                console.log(data.message);

                if (data.message == "ok")
                    $scope.authors.push(currentAuthor);
                else
                    $scope.error = data.message;
                //reset form
                $scope.author = null;
                // $scope.AddAuthorForm.$setPristine(); //for form reset

                $('#AuthorSaveModel').modal('hide');
            }).error(function (data) {
                $scope.error = "An Error has occured while Adding user! " + data.ExceptionMessage;
            });
        }
    };

    //edit user
    $scope.edit = function () {

        $scope.author = this.author;
        //add by rakibul 1-05-17
        console.log('Edit mode=true editText readonly true');
        $scope.editMode = true;
        $scope.editText = true;
        //
        $('#AuthorSaveModel').modal('show');
    };

    //update user
    $scope.update = function () {
        var currentAuthor = this.author;
        bookStoreFactory.updateUser(currentAuthor).success(function (data) {
            currentAuthor.editMode = false;
            $('#AuthorSaveModel').modal('hide');

            if (data.message != "ok")
                $scope.error = data.message;

        }).error(function (data) {
            $scope.error = "An Error has occured while Updating user! " + data.ExceptionMessage;
        });
    };

    // delete User
    $scope.delete = function () {
        currentAuthor = $scope.author;
        bookStoreFactory.deleteUser(currentAuthor).success(function (data) {
            $('#confirmModal').modal('hide');
            $scope.authors.pop(currentAuthor);

        }).error(function (data) {
            $scope.error = "An Error has occured while Deleting user! " + data.ExceptionMessage;

            $('#confirmModal').modal('hide');
        });
    };

    //Model popup events
    $scope.showadd = function () {
        console.log('show add click');
        $scope.author = null;
        $scope.editMode = false;
        $scope.editText = false; //all textbox readonly=false add by rakibul 1-5-17
        $('#AuthorSaveModel').modal('show');
    };

    $scope.showedit = function () {
        //add by rakibul 1-05-17
        console.log('show edit click');
        $scope.editMode = true;
        $scope.editText = true;
        //
        $('#AuthorSaveModel').modal('show');
    };

    $scope.showconfirm = function (data) {
        $scope.author = data;
        $('#confirmModal').modal('show');
    };

    $scope.cancel = function () {
        $scope.author = null;
        $('#AuthorSaveModel').modal('hide');
    }

    // initialize your users data
 
    $scope.getAll();

});

