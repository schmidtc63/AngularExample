// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralWrongQuotes

var aps = angular.module("FanSignupApp",  ['ngAnimate', 'ui.bootstrap']);
aps.factory('FanSignupFactory', 
    function($http) {
        return {
            getSignupInfo: function(onSuccess, onFailure) {
                const rvtoken = $("input[name='__RequestVerificationToken']").val();
                $http({
                    method: "post",
                    url: "/DesktopModules/MVC/LH.FanSignup/FanSignup/GetSignup",
                    headers: {
                        "ModuleId": moduleId,
                        "TabId": tabId,
                        "RequestVerificationToken": rvtoken
                    }
                }).success(onSuccess).error(onFailure);
            },
            saveSignup: function(onSuccess, onFailure, signupInfo) {
                const rvtoken = $("input[name='__RequestVerificationToken']").val();

                $.ajax({
                    cache: false,
                    dataType: 'json',
                    url: "/DesktopModules/MVC/LH.FanSignup/FanSignup/SaveSignup",
                    method: "Post",
                    headers: {
                        "ModuleId": moduleId,
                        "TabId": tabId,
                        "RequestVerificationToken": rvtoken
                    },
                    data: { "data": JSON.stringify(signupInfo) }
                }).success(onSuccess).error(onFailure);
            },
            saveSignupAng: function(onSuccess, onFailure, signupInfo) {
                const rvtoken = $("input[name='__RequestVerificationToken']").val();

                $http.post("/DesktopModules/MVC/LH.FanSignup/FanSignup/SaveSignupAng",
                    JSON.stringify(signupInfo),
                    {
                        headers: {
                            "ModuleId": moduleId,
                            "TabId": tabId,
                            "RequestVerificationToken": rvtoken,
                            'Content-Type': 'application/json'
                        }
                    }).success(onSuccess).error(onFailure);

                /*$http(
                    {
                        url: "/DesktopModules/AdvancedProductSearchAPI/API/AdvancedProductSearchApi/DoAdvancedSearchANG",
                        data: JSON.stringify(searchTerms),
                        method: "post",
                        headers: {
                            "ModuleId": moduleId,
                            "TabId": tabId,
                            "RequestVerificationToken": rvtoken,
                            'Content-Type': 'application/json'
                        }
                    }).success(onSuccess).error(onFailure);
                    */
            }           
        };
    });

aps.controller('FanSignupCtl',
    function(FanSignupFactory, $scope) {
        $scope.Status = false;
        $scope.showErrors=false;
        $scope.showMain = false;
        $scope.emailFormat=/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        //$scope.emailFormat=/^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
        //$scope.emailFormat=/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        $scope.saveSignup = function() {
            if (!$scope.dvFanSignup.$valid) {
                $scope.Message = "Form not complete.";
                $scope.Status = false;
                $scope.showErrors=true;
                return;
            }
            successFunction = function(data) {
                if (data.status) {
                    $scope.Message = "";
                    $scope.showErrors = false;
                } else {
                    if (data.message !== null) {
                        $scope.Status = data.status;
                        $scope.showErrors = true;
                        $scope.Message = data.message;
                    }
                }
            };

            failureFunction = function(data) {
                console.log('Error' + data);
            };
            $scope.emptyOrNull = function(item) {
                return !(item.DisplayText === null || item.DisplayText.trim().length === 0);
            };
            FanSignupFactory.saveSignup(successFunction, failureFunction,$scope.signupInfo);
            //FanSignupFactory.saveSignupAng(successFunction, failureFunction,$scope.signupInfo);
        };

        $scope.getSignup = function() {
            $scope.signupInfo = [];
            $scope.oneAtATime = true;

            $scope.status = {
                isFirstOpen: true,
                isFirstDisabled: false
            };
            const successFunction = function(data) {
                $scope.signupInfo = data;
            };

            const failureFunction = function(data) {
                console.log('Error' + data);
            };
            $scope.emptyOrNull = function(item) {
                return !(item.DisplayText === null || item.DisplayText.trim().length === 0);
            };

            FanSignupFactory.getSignupInfo(successFunction, failureFunction, $scope.signupInfo);
            $scope.showMain = true;
        };
        $scope.resetForm = function() {
           $scope.getSignup();
        };
    });

