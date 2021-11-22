Feature: Weather Prediction
    The weather prediction service allows users to predict the weather
      
Scenario: Can predicate a storm
    Given there is a storm coming
    When a request is made to predict the weather
    Then a storm is correctly predicted
                
        
        
    