/* Module MailingAddressApp 
 * Author: Udod O.V.
 **/

angular.module("MailingAddressApp").controller('MailAdsListCtrl',
['$scope', 'MailAdsListModel', 'orderByFilter', 'mailAddressListFilterFilter', 'filterFilter', 'translationService', MailAdsListCtrl])
.factory('MailAdsListModel', MailAdsListModel)
.filter('mailAddressListFilter', mailAddressListFilter)
.filter('dateRangeFilter', ['dateFilter', dateRangeFilter])
.service('translationService', translationService);

/* Main controller */
function MailAdsListCtrl($scope, MailAdsListModel, orderBy, mailAddressListFilter, filter, translationService) {
    
    $scope.mailAddressesList = null;  // unsorted and non filtered list of addresses
    $scope.total = 0;

    MailAdsListModel.init().then(function (data) {
        //console.log(data.data);
        $scope.total = data.data.count;
        $scope.rangeData.min = $scope.rangeData.valueFrom = data.data.MinHouseNumberFilter;
        $scope.rangeData.max = $scope.rangeData.valueUntil = data.data.MaxHouseNumberFilter;
        $scope.creationDateFilterData.dateFrom = $scope.creationDateFilterData.minDate = data.data.MinCreationDate.slice(6, -2);
        $scope.creationDateFilterData.dateUntil = $scope.creationDateFilterData.maxDate = data.data.MaxCreationDate.slice(6, -2);
        getMailAddressList();
    }, function (error) {
        alert("Error! Can't init app! " + error.status);
    });


    getMailAddressList = function () {
        MailAdsListModel.getMailAddressesList($scope.paginationData.currentPage, $scope.paginationData.itemsPerPage, $scope.totalItems, $scope.sortField, $scope.reverse).then(function (data) {  // download next page of data
            $scope.mailAddressesList = data.data.MailAddressesList;
            $scope.paginationData.currentPage = data.data.PageInfo.PageNumber;
            $scope.paginationData.itemsPerPage = data.data.PageInfo.PageSize;
            $scope.paginationData.totalItems = data.data.PageInfo.TotalItems;
            $scope.sortData.sortField = data.data.PageInfo.OrderField;
            $scope.sortData.reverse = data.data.PageInfo.SortReverse;
            //console.log(data.data.PageInfo);
        }, function (error) {
            alert("Error! Can't get mail address list data! " + error.status);
        });
    };


    /* Localization */
    $scope.translate = function () {
        $scope.selectedLanguage = $scope.radioModel;
        translationService.getTranslation($scope, $scope.selectedLanguage);
    };
    $scope.radioModel = 'ru';
    $scope.translate();


    /* Set bounderies of date and numeric range components by min and max values of all records */
    function SetBounders() {
        var max = $scope.mailAddressesList[0].HouseNumber;
        var min = $scope.mailAddressesList[0].HouseNumber;
        var minDate = $scope.mailAddressesList[0].CreationDate;
        $scope.mailAddressesList.forEach(function (item) {
            if (item.HouseNumber > max) {
                max = item.HouseNumber;
            }
            if (item.HouseNumber < min) {
                min = item.HouseNumber;
            }
            if (item.CreationDate.slice(6, -2) < minDate.slice(6, -2)) {
                minDate = item.CreationDate;
            }
        });
        $scope.rangeData.min = $scope.rangeData.valueFrom = min;
        $scope.rangeData.max = $scope.rangeData.valueUntil = max;
        $scope.creationDateFilterData.dateFrom = $scope.creationDateFilterData.minDate = minDate.slice(6, -2);
        $scope.creationDateFilterData.dateUntil = new Date();
    };
    
    /* Calls when any filter input changed */
    $scope.onFilterChange = function () {
        MailAdsListModel.filterMailAddressesList($scope.countryFilter, $scope.cityFilter, $scope.streetFilter, $scope.rangeData.valueFrom,
                                                 $scope.rangeData.valueUntil, $scope.rangeData.min, $scope.rangeData.max, $scope.indexFilter,
                                                 $scope.creationDateFilterData.dateFrom, $scope.creationDateFilterData.dateUntil,
                                                 $scope.creationDateFilterData.minDate, $scope.creationDateFilterData.maxDate).then(function (data) {
            $scope.mailAddressesList = data.data.MailAddressesList;
            $scope.paginationData.currentPage = data.data.PageInfo.PageNumber;
            $scope.paginationData.itemsPerPage = data.data.PageInfo.PageSize;
            $scope.paginationData.totalItems = data.data.PageInfo.TotalItems;
            $scope.sortData.sortField = data.data.PageInfo.OrderField;
            $scope.sortData.reverse = data.data.PageInfo.SortReverse;
            //console.log(data.data.PageInfo);
        }, function (error) {
            alert("Error! Can't get mail address list data! " + error.status);
        });
        //$scope.filteredData = $scope.mailAddressesList;
        //$scope.filteredData = mailAddressListFilter($scope.filteredData, $scope.rangeData.valueFrom, $scope.rangeData.valueUntil,
        //                                            $scope.creationDateFilterData.dateFrom, $scope.creationDateFilterData.dateUntil);
        //$scope.filteredData = filter($scope.filteredData, { Country: $scope.countryFilter, City: $scope.cityFilter, Street: $scope.streetFilter, Index: $scope.indexFilter });
        //$scope.filteredData = orderBy($scope.filteredData, $scope.sortField, $scope.reverse);
        //$scope.paginationData.totalItems = $scope.filteredData.length;
        //$scope.paginationData.onPageChange();
    };

    /* Reset all filters */
    $scope.resetFilter = function () {
        $scope.countryFilter = undefined;
        $scope.cityFilter = undefined;
        $scope.streetFilter = undefined;
        $scope.indexFilter = undefined;
        $scope.rangeData.reset();
        $scope.rangeData.onRangeChange();
        $scope.creationDateFilterData.reset();
        //$scope.filteredData = $scope.mailAddressesList;
        //$scope.filteredData = orderBy($scope.filteredData, $scope.sortField, $scope.reverse);
        //$scope.paginationData.totalItems = $scope.filteredData.length;
        //$scope.paginationData.onPageChange();

    };

    /* Date Filter properties */
    $scope.creationDateFilterData = {
        dateFrom: undefined,
        dateUntil: undefined,
        minDate: undefined,
        onDateChange: function () {
            $scope.onFilterChange();
        },
        reset: function () {
            this.dateFrom = this.minDate;
            this.dateUntil = new Date();
        }
    };

    /* Numeric range Filter properties */
    $scope.houseNumberFilterData = {
        houseNumberFilter: undefined,
        onHouseNumberFilterChange: function () {
            var res = this.houseNumberFilter.split(/\s*-\s*/, 2);
            if (res.length > 0 && res[0]) {
                $scope.rangeData.valueFrom = parseInt(res[0]);
                $scope.rangeData.valueUntil = $scope.rangeData.valueFrom;
            } else {
                $scope.rangeData.valueFrom = $scope.rangeData.min;
                $scope.rangeData.valueUntil = $scope.rangeData.max;
            }
            if (res.length > 1 && res[1]) {
                $scope.rangeData.valueUntil = parseInt(res[1]);
            }
            $scope.onFilterChange();
        }
    };

    /* Range component properties */
    $scope.rangeData = {
        valueFrom: 1,
        valueUntil: 999,
        min: 1,
        max: 999,
        onRangeChange: function () {
            $scope.houseNumberFilterData.houseNumberFilter = this.valueFrom + " - " + this.valueUntil;
            $scope.onFilterChange();
        },
        isFiltred: function () {
            return !(this.valueFrom === this.min && this.valueUntil === this.max);
        },
        reset: function () {
            $scope.rangeData.valueFrom = $scope.rangeData.min;
            $scope.rangeData.valueUntil = $scope.rangeData.max;
        }
    };

    /* Pagination properties */
    $scope.paginationData = {
        maxSize: 5,
        itemsPerPage: 10,
        totalItems: 0,
        currentPage: 1,
        onPageChange: function () {
            getMailAddressList();
            //$scope.pageData = $scope.filteredData.slice((this.currentPage - 1) * this.itemsPerPage, this.currentPage * this.itemsPerPage);
        }
    };

    /* Sorting properties */
    $scope.sortData = {
        sortField: 'CreationDate',
        reverse: true,
        sort: function(fieldName) {
            $scope.reverse = (fieldName !== null && this.sortField === fieldName) ? !this.reverse : false;
            this.sortField = fieldName;
            //$scope.filteredData = orderBy($scope.filteredData, $scope.sortField, $scope.reverse);
            //$scope.paginationData.onPageChange();
        },
        isSortUp: function (fieldName) {
            return this.sortField === fieldName && this.reverse;
        },
        isSortDown: function (fieldName) {
            return this.sortField === fieldName && this.reverse;
        }
    };
    /*$scope.sortField = 'CreationDate';
    $scope.reverse = true;

    $scope.sort = function (fieldName) {
        $scope.reverse = (fieldName !== null && $scope.sortField === fieldName) ? !$scope.reverse : false;
        $scope.sortField = fieldName;
        $scope.filteredData = orderBy($scope.filteredData, $scope.sortField, $scope.reverse);
        $scope.paginationData.onPageChange();
    };
    $scope.isSortUp = function (fieldName) {
        return $scope.sortField === fieldName && !$scope.reverse;
    };*/
    $scope.isSortDown = function (fieldName) {
        return $scope.sortField === fieldName && $scope.reverse;
    };

    //getMailAddressList();

};

/* Filter for date and numeric range */
function mailAddressListFilter() {
    return function (input, valueFrom, valueUntil, dateFrom, dateUntil) {
        return input.filter(function (item) {
            return (item.HouseNumber >= valueFrom) && (item.HouseNumber <= valueUntil) && (item.CreationDate.slice(6, -2) >= dateFrom) && (item.CreationDate.slice(6, -2) <= dateUntil);
        });
    };
};

/* Filter for show rigth format date in filter input */
function dateRangeFilter(dateFilter) {
    return function (input) {
        if (!input.dateFrom) {
            input.dateFrom = input.minDate;
        }
        if (!input.dateUntil) {
            input.dateUntil = new Date();
        }
        return dateFilter(input.dateFrom, 'dd.MM.yyyy') + "-" + dateFilter(input.dateUntil, 'dd.MM.yyyy');
    }
}

/* Localization Service */
function translationService() {
    this.getTranslation = function ($scope, language) {
        var data = null;
        switch (language) {
            case 'en':
                data = {
                    "CAPTION_COUNTRY": "Country",
                    "CAPTION_CITY": "City",
                    "CAPTION_STREET": "Street",
                    "CAPTION_HN": "House #",
                    "CAPTION_INDEX": "Index",
                    "CAPTION_DATE": "Date",
                    "TITLE": "Mail addresses table",
                    "INPUT_RANGE": "Input number range",
                    "RANGE": "Input date range",
                    "PASS": "Records",
                    "FROM": "of total",
                    "PREV": "Previous",
                    "NEXT": "Next"
                };
                break;
            case 'ru':
                data = {
                    "CAPTION_COUNTRY": "Страна",
                    "CAPTION_CITY": "Город",
                    "CAPTION_STREET": "Улица",
                    "CAPTION_HN": "Дом №",
                    "CAPTION_INDEX": "Индекс",
                    "CAPTION_DATE": "Дата",
                    "TITLE": "Таблица почтовых адресов",
                    "INPUT_RANGE": "Введите диапозон номеров",
                    "RANGE": "Укажите период",
                    "PASS": "Обработано",
                    "FROM": "из",
                    "PREV": "Предыдущая",
                    "NEXT": "Следующая"
                };
                break;
        }
        $scope.translation = data;
    };
}
