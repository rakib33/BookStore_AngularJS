
var app = angular.module('bookStoreApp', ['angularUtils.directives.dirPagination', 'ngRoute','ngFileUpload']);
var baseAddress = 'http://localhost:55170/';
var url = "";

//angularUtils.directives.dirPagination this directive is for pagination

// configure our routes
app.config(function($routeProvider) {
        $routeProvider

            // route for the home page
            .when('/', {
                templateUrl : 'Pages/Author.html',
                controller: 'AuthorController'
            })

            // route for the about page
            .when('/Author', {
                templateUrl: 'Pages/Author.html',
                controller: 'AuthorController'
            })
            // route for the Book page
            .when('/Book', {
                templateUrl: 'Pages/Book.html',
                controller: 'BookController'
            });
    });



app.factory('AuthorStoreFactory', function ($http) {
    return {
        getAuthorsList: function () {
            url = baseAddress + "author/list";
            return $http.get(url);
        },
        getAuthor: function (author) {
            url = baseAddress + "author/getAuthor/" + author.Id;
            return $http.get(url);
        },
        addAuthor: function (author) {
            url = baseAddress + "author/post";
            return $http.post(url, author);
        },
        deleteAuthor: function (author) {
            url = baseAddress + "DeleteAuthor/" + author.Id;
            return $http.delete(url);
        },
        updateAuthor: function (author) {
            url = baseAddress + "author/Update/" + author.Id;
            return $http.put(url, author);
        }
    };
});

app.controller('AuthorController', function PostController($scope, $http, AuthorStoreFactory) {  //i add $http, for paging
          
    $scope.isLoading = false;
   
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

    //get all Author
    $scope.getAll = function () {
        console.log('call data list');
        $scope.isLoading = true;

        AuthorStoreFactory.getAuthorsList().success(function (data) {
            console.log(data.list);
            $scope.authors = data.list;
       
        }).error(function (data) {
            $scope.error = "An Error has occured while Loading users! " + data.ExceptionMessage;
        });

        $scope.isLoading = false;
        //add for pagination
        $scope.sort = function (keyname) {
            $scope.sortKey = keyname;   //set the sortKey to the param passed
            $scope.reverse = !$scope.reverse; //if true make it false and vice versa
        }
    };

    // add Author
    $scope.add = function () {
        var currentAuthor = this.author;
        if (currentAuthor != null && currentAuthor.FirstName != null && currentAuthor.Address && currentAuthor.LastName && currentAuthor.ZipCode && currentAuthor.Country && currentAuthor.Initials) {
            AuthorStoreFactory.addAuthor(currentAuthor).success(function (data) {

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

    //edit Author
    $scope.edit = function () {

        $scope.author = this.author;
        //add by rakibul 1-05-17
        console.log('Edit mode=true editText readonly true');
        $scope.editMode = true;
        $scope.editText = true;
        //
        $('#AuthorSaveModel').modal('show');
    };

    //update Author
    $scope.update = function () {
        var currentAuthor = this.author;
        AuthorStoreFactory.updateAuthor(currentAuthor).success(function (data) {
            currentAuthor.editMode = false;
            $('#AuthorSaveModel').modal('hide');

            if (data.message != "ok")
                $scope.error = data.message;

        }).error(function (data) {
            $scope.error = "An Error has occured while Updating user! " + data.ExceptionMessage;
        });
    };

    // delete Author
    $scope.delete = function () {
        currentAuthor = $scope.author;
        AuthorStoreFactory.deleteAuthor(currentAuthor).success(function (data) {
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



app.factory('BookStoreFactory', function ($http) {
    return {
        getBooksList: function () {
            url = baseAddress + "book/list";
            return $http.get(url);
        },
        getBook: function (book) {
            url = baseAddress + "book/getbook/" + book.Id;
            return $http.get(url);
        },
        addBook: function (book) {
            url = baseAddress + "book/post";
            return $http.post(url, book);
        },
        deleteBook: function (book) {
            url = baseAddress + "DeleteBook/" + book.Id;
            return $http.delete(url);
        },
        updateBook: function (book) {
            url = baseAddress + "book/Update/" + book.Id;
            return $http.put(url, book);
        }
    };
});


app.controller('BookController', function ($scope, $http, $timeout, $compile, Upload, BookStoreFactory) {
   // $scope.message = 'This is a book Controller.';

    //below is responsible ti display selected Image
    $scope.onChange = function (files) {
        if (files[0] == undefined) return;
        $scope.fileExt = files[0].name.split(".").pop()
    }
    $scope.isImage = function (ext) {
        if (ext) {
            return ext == "jpg" || ext == "jpeg" || ext == "gif" || ext == "png"||ext=="JPG"
        }
    }
    //End display selected Image

    $scope.isLoading = false;

    $scope.books = [];
    $scope.book = null;
    $scope.editMode = false;

    //get User display author when row click
    $scope.get = function () {
        $scope.book = this.book;
        $scope.editMode = true;
        $scope.editText = true;

        console.log('table row click');
        $('#BookSaveModel').modal('show');
        //$('#bookViewModal').modal('show');
    };

    //get all Author
    $scope.getAll = function () {
        console.log('call data list');
        $scope.isLoading = true;

        BookStoreFactory.getBooksList().success(function (data) {
            console.log(data.list);
            if (data.message == "ok") {
                $scope.books = data.list;
            } else {
                $scope.error = data.message;
            }

        }).error(function (data) {
            $scope.error = "An Error has occured while Loading users! " + data.ExceptionMessage;
        });

        $scope.isLoading = false;
        //add for pagination
        $scope.sort = function (keyname) {
            $scope.sortKey = keyname;   //set the sortKey to the param passed
            $scope.reverse = !$scope.reverse; //if true make it false and vice versa
        }
    };

    // add Author
    $scope.add = function () {
        var currentBook = this.book;
        if (currentBook != null && currentBook.Title != null && currentBook.ISBN && currentBook.PublishingDate && currentBook.Price && currentBook.PublishingHouse) {
            BookStoreFactory.addBook(currentBook).success(function (data) {

                $scope.addMode = false; // 
                $scope.editText = false;
                currentBook.Id = data.id;

                console.log(data.message);

                if (data.message == "ok")
                    $scope.books.push(currentBook);
                else
                    $scope.error = data.message;
                //reset form
                $scope.book = null;

                $('#BookSaveModel').modal('hide');
            }).error(function (data) {
                $('#BookSaveModel').modal('hide');
                $scope.error = "An Error has occured while Adding user! " + data.ExceptionMessage;
             
            });
        }
        else {
            $scope.error = "Invalid Provided data.";
            $('#BookSaveModel').modal('hide');
        }
    };

    //edit Book
    $scope.edit = function () {

        $scope.book = this.book;
        //add by rakibul 10-05-17
        console.log('Edit mode=true editText readonly true');
        $scope.editMode = true;
        $scope.editText = true;
        //
        $('#BookSaveModel').modal('show');
    };

    //update Book
    $scope.update = function () {
        var currentBook = this.book;
        BookStoreFactory.updateBook(currentBook).success(function (data) {
            currentBook.editMode = false;
           
            if (data.message != "ok")
                $scope.error = data.message;

        }).error(function (data) {
            $scope.error = "An Error has occured while Updating user! " + data.ExceptionMessage;
        });

        $('#BookSaveModel').modal('hide');
    };

    // delete Book
    $scope.delete = function () {
        currentBook = $scope.book;
        BookStoreFactory.deleteUser(currentBook).success(function (data) {
            $('#confirmModal').modal('hide');
            $scope.books.pop(currentBook);

        }).error(function (data) {
            $scope.error = "An Error has occured while Deleting book! " + data.ExceptionMessage;
            $('#confirmModal').modal('hide');
        });
    };

    //Model popup events
    $scope.showadd = function () {
        console.log('show add click');
        $scope.book = null;
        $scope.editMode = false;
        $scope.editText = false; //all textbox readonly=false add by rakibul 10-5-17
        $('#BookSaveModel').modal('show');
    };

    $scope.showedit = function () {
        //add by rakibul 10-05-17
        console.log('show edit click');
        $scope.editMode = true;
        $scope.editText = true;
        //
        $('#BookSaveModel').modal('show');
    };

    $scope.showconfirm = function (data) {
        $scope.book = data;
        $('#confirmModal').modal('show');
    };

    $scope.cancel = function () {
        $scope.book = null;
        $('#BookSaveModel').modal('hide');
    }

    // initialize your users data

    $scope.getAll();


});



