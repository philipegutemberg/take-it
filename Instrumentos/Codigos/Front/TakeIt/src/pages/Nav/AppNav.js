import React, { useContext } from 'react';
import { StatusBar } from 'react-native';
import { NavigationContainer } from '@react-navigation/native';
import Routes from '../../routes'
import Spinner from 'react-native-loading-spinner-overlay';
import { LoadingContext } from '../../context/LoadingContext';

export default function AppNav() {  
  const {isLoading} = useContext(LoadingContext);

  return (
    <NavigationContainer>
      <StatusBar backgroundColor="#7ED957" barStyle="light-content" />
      <Spinner 
          visible={isLoading}
          size='large'
          color='#d957d0'
        />
      <Routes/>
    </NavigationContainer>
  ); 
}
