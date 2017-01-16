var myApp = angular.module("myApp", []);
myApp.controller('mainController', ['$scope', '$http', function ($scope, $http) {

    $http.get("/Home/GetEmployeeDetails")
    .success(function (result) {

        $scope.employeeDetails = result;
        if(result.length ==0)
        {
            window.location= "/Home/UploadDetails"
        }
    })
    .error(function (error) {
        console.log("Error");
    });
}]);

